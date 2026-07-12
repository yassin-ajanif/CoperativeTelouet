using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Client;
using CoperativeTelouet.Domain.Enums;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Client;

public class ClientService
    : GenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>,
      IClientService
{
    private readonly IRepository<FactureClient> _factures;
    private readonly IRepository<PaiementClient> _paiements;
    private readonly IRepository<AvoirClient> _avoirs;
    private readonly IRepository<AvoirClientLigne> _avoirLignes;

    public ClientService(
        IRepository<Tiers> tiers,
        IRepository<FactureClient> factures,
        IRepository<PaiementClient> paiements,
        IRepository<AvoirClient> avoirs,
        IRepository<AvoirClientLigne> avoirLignes,
        IMapper mapper,
        IEnumerable<IValidator<CreateTiersDto>> createValidators,
        IEnumerable<IValidator<UpdateTiersDto>> updateValidators)
        : base(tiers, mapper, createValidators, updateValidators)
    {
        _factures = factures;
        _paiements = paiements;
        _avoirs = avoirs;
        _avoirLignes = avoirLignes;
    }

    public Task<PagedResult<TiersDto>> GetClientsAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
    {
        var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

        return QueryPagedAsync(
            t => (t.Type == TypeTiers.Client || t.Type == TypeTiers.LesDeux)
                 && (q == null
                     || t.Nom.Contains(q)
                     || (t.ICE != null && t.ICE.Contains(q))
                     || (t.Ville != null && t.Ville.Contains(q))
                     || (t.Telephone != null && t.Telephone.Contains(q))),
            query => query.OrderBy(t => t.Nom),
            t => new TiersDto(
                t.Id,
                t.Type,
                t.Nom,
                t.ICE,
                t.Adresse,
                t.Ville,
                t.Telephone,
                t.Email,
                t.ConditionsPaiement,
                t.Actif),
            page,
            pageSize,
            cancellationToken);
    }

    public async Task<TiersDto?> GetClientByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var dto = await GetByIdAsync(id, cancellationToken);
        if (dto is null || !IsClientSide(dto))
            return null;
        return dto;
    }

    public async Task<TiersDto> CreateClientAsync(
        CreateTiersDto dto,
        CancellationToken cancellationToken = default)
    {
        EnsureClientType(dto.Type);
        return await CreateAsync(dto, cancellationToken);
    }

    public async Task UpdateClientAsync(
        int id,
        UpdateTiersDto dto,
        CancellationToken cancellationToken = default)
    {
        EnsureClientType(dto.Type);
        _ = await GetClientByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Client {id} introuvable.");
        await UpdateAsync(id, dto, cancellationToken);
    }

    public async Task DeleteClientAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetClientByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Client {id} introuvable.");
        await DeleteAsync(id, cancellationToken);
    }

    public async Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default)
    {
        var dto = await GetClientByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Client {id} introuvable.");

        await UpdateAsync(
            id,
            Mapper.Map<UpdateTiersDto>(dto) with { Actif = !dto.Actif },
            cancellationToken);

        return (await GetByIdAsync(id, cancellationToken))!;
    }

    public async Task<ClientCompteDto> GetCompteAsync(int clientId, CancellationToken cancellationToken = default)
    {
        var factures = await _factures.FindAsync(f => f.ClientId == clientId, cancellationToken);
        var factureIds = factures.Select(f => f.Id).ToList();

        var paiements = factureIds.Count == 0
            ? (IReadOnlyList<PaiementClient>)[]
            : await _paiements.FindAsync(p => factureIds.Contains(p.FactureClientId), cancellationToken);

        var avoirs = await _avoirs.FindAsync(a => a.ClientId == clientId, cancellationToken);
        var avoirIds = avoirs.Select(a => a.Id).ToList();

        var avoirLignes = avoirIds.Count == 0
            ? (IReadOnlyList<AvoirClientLigne>)[]
            : await _avoirLignes.FindAsync(l => avoirIds.Contains(l.AvoirClientId), cancellationToken);

        var avoirTotals = avoirLignes
            .GroupBy(l => l.AvoirClientId)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(l => LineTtc(l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));

        var raw = new List<(DateTime Date, string Designation, string? Observation, decimal Debit, decimal Credit)>();

        foreach (var f in factures)
            raw.Add((f.Date, $"FACTURE N°{f.Numero}", f.Note, f.TotalTtc, 0m));

        foreach (var p in paiements)
            raw.Add((p.Date, PaiementDesignation(p.Mode), p.Reference, 0m, p.Montant));

        foreach (var a in avoirs)
        {
            var total = avoirTotals.GetValueOrDefault(a.Id);
            raw.Add((a.Date, $"AVOIR N°{a.Numero}", a.Motif, 0m, total));
        }

        var ordered = raw.OrderBy(x => x.Date).ThenBy(x => x.Designation).ToList();
        decimal running = 0;
        var lignes = new List<ClientCompteLigneDto>(ordered.Count);
        foreach (var row in ordered)
        {
            running += row.Debit - row.Credit;
            lignes.Add(new ClientCompteLigneDto(row.Date, row.Designation, row.Observation, row.Debit, row.Credit, running));
        }

        return new ClientCompteDto(running, lignes);
    }

    private static bool IsClientSide(TiersDto t) =>
        t.Type is TypeTiers.Client or TypeTiers.LesDeux;

    private static void EnsureClientType(TypeTiers type)
    {
        if (type is not (TypeTiers.Client or TypeTiers.LesDeux))
            throw new InvalidOperationException("Le type doit être Client ou LesDeux.");
    }

    private static decimal LineTtc(decimal qty, decimal puHt, decimal remise, decimal tva)
    {
        var ht = qty * puHt - remise;
        return ht * (1 + tva / 100m);
    }

    private static string PaiementDesignation(ModePaiement mode) => mode switch
    {
        ModePaiement.Especes => "ESPÈCES REÇUES",
        ModePaiement.Cheque => "CHÈQUE REÇU",
        ModePaiement.TPE => "TPE REÇU",
        ModePaiement.Virement => "VIREMENT REÇU",
        ModePaiement.Effet => "EFFET REÇU",
        ModePaiement.Credit => "CRÉDIT",
        _ => $"{mode.ToString().ToUpperInvariant()} REÇU",
    };
}
