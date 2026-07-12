using CoperativeTelouet.Business.Services;
using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Client;
using CoperativeTelouet.Domain.Enums;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Client.BonCommande;

public class BonCommandeClientService
    : GenericService<BonCommandeClient, BonCommandeClientDto, CreateBonCommandeClientDto, UpdateBonCommandeClientDto>,
      IBonCommandeClientService
{
    private readonly IRepository<BonCommandeClientLigne> _lignes;
    private readonly IRepository<Tiers> _tiers;
    private readonly IArticleSuggestionService _articles;

    public BonCommandeClientService(
        IRepository<BonCommandeClient> bons,
        IRepository<BonCommandeClientLigne> lignes,
        IRepository<Tiers> tiers,
        IArticleSuggestionService articles,
        IMapper mapper,
        IEnumerable<IValidator<CreateBonCommandeClientDto>> createValidators,
        IEnumerable<IValidator<UpdateBonCommandeClientDto>> updateValidators)
        : base(bons, mapper, createValidators, updateValidators)
    {
        _lignes = lignes;
        _tiers = tiers;
        _articles = articles;
    }

    public Task<PagedResult<BonCommandeClientListItemDto>> GetBonsCommandeAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
    {
        var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();
        var from = dateFrom?.Date;
        var toExclusive = dateTo?.Date.AddDays(1);

        return QueryPagedAsync(
            d => (from == null || d.Date >= from)
                 && (toExclusive == null || d.Date < toExclusive)
                 && (q == null
                     || d.Numero.Contains(q)
                     || d.Client.Nom.Contains(q)),
            query => query.OrderByDescending(d => d.Date).ThenByDescending(d => d.Id),
            d => new BonCommandeClientListItemDto(
                d.Id,
                d.Numero,
                d.ClientId,
                d.Client.Nom,
                d.Date,
                d.TotalTtc,
                d.Note),
            page,
            pageSize,
            cancellationToken);
    }

    public async Task<BonCommandeClientDto?> GetBonCommandeByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var dto = await GetByIdAsync(id, cancellationToken);
        if (dto is null)
            return null;

        var lignes = await _lignes.FindAsync(l => l.BonCommandeClientId == id, cancellationToken);
        return dto with
        {
            Lignes = lignes.Select(l => Mapper.Map<BonCommandeClientLigneDto>(l)).ToList(),
        };
    }

    public async Task<BonCommandeClientDto> CreateBonCommandeAsync(
        CreateBonCommandeClientDto dto,
        CancellationToken cancellationToken = default)
    {
        await EnsureClientExistsAsync(dto.ClientId, cancellationToken);

        var numero = string.IsNullOrWhiteSpace(dto.Numero)
            ? await GenerateNumeroAsync(cancellationToken)
            : dto.Numero.Trim();

        var (_, _, ttc) = DocumentTotals.Compute(
            dto.Lignes.Select(l => (l.QuantiteCommandee, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));

        var created = await CreateAsync(
            dto with { Numero = numero, TotalTtc = ttc },
            cancellationToken);

        return (await GetBonCommandeByIdAsync(created.Id, cancellationToken))!;
    }

    public async Task UpdateBonCommandeAsync(
        int id,
        UpdateBonCommandeClientDto dto,
        CancellationToken cancellationToken = default)
    {
        await EnsureClientExistsAsync(dto.ClientId, cancellationToken);

        var (_, _, ttc) = DocumentTotals.Compute(
            dto.Lignes.Select(l => (l.QuantiteCommandee, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));
        var toUpdate = dto with { TotalTtc = ttc };
        await ValidateAsync(UpdateValidator, toUpdate, cancellationToken);

        var entity = await Repo.GetByIdWithNavigationsAsync(
                id,
                [d => d.Lignes],
                cancellationToken)
            ?? throw new KeyNotFoundException($"Bon de commande {id} introuvable.");

        entity.Lignes.Clear();
        Mapper.Map(toUpdate, entity);

        await Repo.UpdateAsync(entity, cancellationToken);
    }

    public async Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default)
    {
        var year = DateTime.Today.Year;
        var prefix = $"BC-{year}-";
        var existing = await FindAsync(d => d.Numero.StartsWith(prefix), cancellationToken);
        var next = existing
            .Select(d =>
            {
                var tail = d.Numero[prefix.Length..];
                return int.TryParse(tail, out var n) ? n : 0;
            })
            .DefaultIfEmpty(0)
            .Max() + 1;

        return $"{prefix}{next:D4}";
    }

    public Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default) =>
        _articles.SearchArticlesAsync(search, cancellationToken);

    private async Task EnsureClientExistsAsync(int clientId, CancellationToken cancellationToken)
    {
        var client = await _tiers.GetByIdAsync(clientId, cancellationToken)
            ?? throw new KeyNotFoundException($"Client {clientId} introuvable.");

        if (client.Type is not (TypeTiers.Client or TypeTiers.LesDeux))
            throw new InvalidOperationException("Le tiers sélectionné n'est pas un client.");
    }
}
