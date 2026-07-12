using CoperativeTelouet.Business.Services;
using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Logging;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Fournisseur;
using CoperativeTelouet.Domain.Enums;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Fournisseur.BonReception;

public class BonReceptionFournisseurService
    : GenericService<BonReceptionFournisseur, BonReceptionFournisseurDto, CreateBonReceptionFournisseurDto, UpdateBonReceptionFournisseurDto>,
      IBonReceptionFournisseurService
{
    private readonly IRepository<BonReceptionFournisseurLigne> _lignes;
    private readonly IRepository<Tiers> _tiers;
    private readonly IArticleSuggestionService _articles;

    public BonReceptionFournisseurService(
        IRepository<BonReceptionFournisseur> bons,
        IRepository<BonReceptionFournisseurLigne> lignes,
        IRepository<Tiers> tiers,
        IArticleSuggestionService articles,
        IMapper mapper,
        IEnumerable<IValidator<CreateBonReceptionFournisseurDto>> createValidators,
        IEnumerable<IValidator<UpdateBonReceptionFournisseurDto>> updateValidators,
        IErrorLogger logger)
        : base(bons, mapper, createValidators, updateValidators, logger)
    {
        _lignes = lignes;
        _tiers = tiers;
        _articles = articles;
    }

    public Task<PagedResult<BonReceptionFournisseurListItemDto>> GetBonsReceptionAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetBonsReceptionAsync), () =>
        {
            var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();
            var from = dateFrom?.Date;
            var toExclusive = dateTo?.Date.AddDays(1);

            return QueryPagedAsync(
                b => (from == null || b.Date >= from)
                     && (toExclusive == null || b.Date < toExclusive)
                     && (q == null
                         || b.Numero.Contains(q)
                         || b.Fournisseur.Nom.Contains(q)),
                query => query.OrderByDescending(b => b.Date).ThenByDescending(b => b.Id),
                b => new BonReceptionFournisseurListItemDto(
                    b.Id,
                    b.Numero,
                    b.FournisseurId,
                    b.Fournisseur.Nom,
                    b.BonCommandeFournisseurId,
                    b.Date,
                    b.TotalTtc,
                    b.Note),
                page,
                pageSize,
                cancellationToken);
        });

    public Task<BonReceptionFournisseurDto?> GetBonReceptionByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetBonReceptionByIdAsync), async () =>
        {
            var dto = await GetByIdAsync(id, cancellationToken);
            if (dto is null)
                return null;

            var lignes = await _lignes.FindAsync(l => l.BonReceptionFournisseurId == id, cancellationToken);
            return dto with
            {
                Lignes = lignes.Select(l => Mapper.Map<BonReceptionFournisseurLigneDto>(l)).ToList(),
            };
        });

    public Task<BonReceptionFournisseurDto> CreateBonReceptionAsync(
        CreateBonReceptionFournisseurDto dto,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(CreateBonReceptionAsync), async () =>
        {
            await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

            var numero = string.IsNullOrWhiteSpace(dto.Numero)
                ? await GenerateNumeroAsync(cancellationToken)
                : dto.Numero.Trim();

            var (_, _, ttc) = DocumentTotals.Compute(
                dto.Lignes.Select(l => (l.QuantiteRecue, l.PrixUnitaireHT, 0m, l.TauxTVA)));
            var created = await CreateAsync(
                dto with { Numero = numero, TotalTtc = ttc },
                cancellationToken);

            return (await GetBonReceptionByIdAsync(created.Id, cancellationToken))!;
        });

    public Task UpdateBonReceptionAsync(
        int id,
        UpdateBonReceptionFournisseurDto dto,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(UpdateBonReceptionAsync), async () =>
        {
            await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

            var (_, _, ttc) = DocumentTotals.Compute(
                dto.Lignes.Select(l => (l.QuantiteRecue, l.PrixUnitaireHT, 0m, l.TauxTVA)));
            var toUpdate = dto with { TotalTtc = ttc };
            await ValidateAsync(UpdateValidator, toUpdate, cancellationToken);

            var entity = await Repo.GetByIdWithNavigationsAsync(
                    id,
                    [b => b.Lignes],
                    cancellationToken)
                ?? throw new KeyNotFoundException($"Bon de réception fournisseur {id} introuvable.");

            entity.Lignes.Clear();
            Mapper.Map(toUpdate, entity);

            await Repo.UpdateAsync(entity, cancellationToken);
        });

    public Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GenerateNumeroAsync), async () =>
        {
            var year = DateTime.Today.Year;
            var prefix = $"BR-{year}-";
            var existing = await FindAsync(b => b.Numero.StartsWith(prefix), cancellationToken);
            var next = existing
                .Select(b =>
                {
                    var tail = b.Numero[prefix.Length..];
                    return int.TryParse(tail, out var n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max() + 1;

            return $"{prefix}{next:D4}";
        });

    public Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(SearchArticlesAsync), () =>
            _articles.SearchArticlesAsync(search, cancellationToken));

    private async Task EnsureFournisseurExistsAsync(int fournisseurId, CancellationToken cancellationToken)
    {
        var fournisseur = await _tiers.GetByIdAsync(fournisseurId, cancellationToken)
            ?? throw new KeyNotFoundException($"Fournisseur {fournisseurId} introuvable.");

        if (fournisseur.Type is not (TypeTiers.Fournisseur or TypeTiers.LesDeux))
            throw new InvalidOperationException("Le tiers sélectionné n'est pas un fournisseur.");
    }
}
