using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Fournisseur;
using CoperativeTelouet.Domain.Enums;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Fournisseur;

public class FournisseurService
    : GenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>,
      IFournisseurService
{
    private readonly IRepository<FactureFournisseur> _factures;
    private readonly IRepository<PaiementFournisseur> _paiements;
    private readonly IRepository<AvoirFournisseur> _avoirs;
    private readonly IRepository<AvoirFournisseurLigne> _avoirLignes;

    public FournisseurService(
        IRepository<Tiers> tiers,
        IRepository<FactureFournisseur> factures,
        IRepository<PaiementFournisseur> paiements,
        IRepository<AvoirFournisseur> avoirs,
        IRepository<AvoirFournisseurLigne> avoirLignes,
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

    public async Task<IReadOnlyList<TiersDto>> GetFournisseursAsync(string? search = null, CancellationToken cancellationToken = default)
    {
        var fournisseurs = await FindAsync(
            t => t.Type == TypeTiers.Fournisseur || t.Type == TypeTiers.LesDeux,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.Trim();
            fournisseurs = fournisseurs.Where(t =>
                t.Nom.Contains(q, StringComparison.OrdinalIgnoreCase)
                || (t.ICE?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false)
                || (t.Ville?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false)
                || (t.Telephone?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
        }

        return fournisseurs.OrderBy(t => t.Nom).ToList();
    }

    public async Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default)
    {
        var dto = await GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Fournisseur {id} introuvable.");
        if (!IsFournisseurSide(dto))
            throw new KeyNotFoundException($"Fournisseur {id} introuvable.");

        EnsureFournisseurType(dto.Type);
        await UpdateAsync(
            id,
            Mapper.Map<UpdateTiersDto>(dto) with { Actif = !dto.Actif },
            cancellationToken);

        return (await GetByIdAsync(id, cancellationToken))!;
    }

    public async Task<FournisseurCompteDto> GetCompteAsync(int fournisseurId, CancellationToken cancellationToken = default)
    {
        var factures = await _factures.FindAsync(f => f.FournisseurId == fournisseurId, cancellationToken);
        var factureIds = factures.Select(f => f.Id).ToList();

        var paiements = factureIds.Count == 0
            ? (IReadOnlyList<PaiementFournisseur>)[]
            : await _paiements.FindAsync(p => factureIds.Contains(p.FactureFournisseurId), cancellationToken);

        var avoirs = await _avoirs.FindAsync(a => a.FournisseurId == fournisseurId, cancellationToken);
        var avoirIds = avoirs.Select(a => a.Id).ToList();

        var avoirLignes = avoirIds.Count == 0
            ? (IReadOnlyList<AvoirFournisseurLigne>)[]
            : await _avoirLignes.FindAsync(l => avoirIds.Contains(l.AvoirFournisseurId), cancellationToken);

        var avoirTotals = avoirLignes
            .GroupBy(l => l.AvoirFournisseurId)
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
        var lignes = new List<FournisseurCompteLigneDto>(ordered.Count);
        foreach (var row in ordered)
        {
            running += row.Debit - row.Credit;
            lignes.Add(new FournisseurCompteLigneDto(row.Date, row.Designation, row.Observation, row.Debit, row.Credit, running));
        }

        return new FournisseurCompteDto(running, lignes);
    }

    private static bool IsFournisseurSide(TiersDto t) =>
        t.Type is TypeTiers.Fournisseur or TypeTiers.LesDeux;

    private static void EnsureFournisseurType(TypeTiers type)
    {
        if (type is not (TypeTiers.Fournisseur or TypeTiers.LesDeux))
            throw new InvalidOperationException("Le type doit être Fournisseur ou LesDeux.");
    }

    private static decimal LineTtc(decimal qty, decimal puHt, decimal remise, decimal tva)
    {
        var ht = qty * puHt - remise;
        return ht * (1 + tva / 100m);
    }

    private static string PaiementDesignation(ModePaiement mode) => mode switch
    {
        ModePaiement.Especes => "ESPÈCES PAYÉES",
        ModePaiement.Cheque => "CHÈQUE PAYÉ",
        ModePaiement.TPE => "TPE PAYÉ",
        ModePaiement.Virement => "VIREMENT PAYÉ",
        ModePaiement.Effet => "EFFET PAYÉ",
        ModePaiement.Credit => "CRÉDIT",
        _ => $"{mode.ToString().ToUpperInvariant()} PAYÉ",
    };
}
