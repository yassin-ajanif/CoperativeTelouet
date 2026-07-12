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

namespace CoperativeTelouet.Business.Services.Fournisseur.Devis;

public class DevisFournisseurService
    : GenericService<DevisFournisseur, DevisFournisseurDto, CreateDevisFournisseurDto, UpdateDevisFournisseurDto>,
      IDevisFournisseurService
{
    private readonly IRepository<DevisFournisseurLigne> _lignes;
    private readonly IRepository<Tiers> _tiers;
    private readonly IArticleSuggestionService _articles;

    public DevisFournisseurService(
        IRepository<DevisFournisseur> devis,
        IRepository<DevisFournisseurLigne> lignes,
        IRepository<Tiers> tiers,
        IArticleSuggestionService articles,
        IMapper mapper,
        IEnumerable<IValidator<CreateDevisFournisseurDto>> createValidators,
        IEnumerable<IValidator<UpdateDevisFournisseurDto>> updateValidators,
        IErrorLogger logger)
        : base(devis, mapper, createValidators, updateValidators, logger)
    {
        _lignes = lignes;
        _tiers = tiers;
        _articles = articles;
    }

    public Task<PagedResult<DevisFournisseurListItemDto>> GetDevisAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetDevisAsync), () =>
        {
            var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();
            var from = dateFrom?.Date;
            var toExclusive = dateTo?.Date.AddDays(1);

            return QueryPagedAsync(
                d => (from == null || d.Date >= from)
                     && (toExclusive == null || d.Date < toExclusive)
                     && (q == null
                         || d.Numero.Contains(q)
                         || d.Fournisseur.Nom.Contains(q)),
                query => query.OrderByDescending(d => d.Date).ThenByDescending(d => d.Id),
                d => new DevisFournisseurListItemDto(
                    d.Id,
                    d.Numero,
                    d.FournisseurId,
                    d.Fournisseur.Nom,
                    d.Date,
                    d.DateValidite,
                    d.TotalTtc,
                    d.Note),
                page,
                pageSize,
                cancellationToken);
        });

    public Task<DevisFournisseurDto?> GetDevisByIdAsync(int id, CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetDevisByIdAsync), async () =>
        {
            var dto = await GetByIdAsync(id, cancellationToken);
            if (dto is null)
                return null;

            var lignes = await _lignes.FindAsync(l => l.DevisFournisseurId == id, cancellationToken);
            return dto with
            {
                Lignes = lignes.Select(l => Mapper.Map<DevisFournisseurLigneDto>(l)).ToList(),
                Conditions = [],
            };
        });

    public Task<DevisFournisseurDto> CreateDevisAsync(
        CreateDevisFournisseurDto dto,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(CreateDevisAsync), async () =>
        {
            await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

            var numero = string.IsNullOrWhiteSpace(dto.Numero)
                ? await GenerateNumeroAsync(cancellationToken)
                : dto.Numero.Trim();

            var (_, _, ttc) = DocumentTotals.Compute(
                dto.Lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)),
                dto.RemiseGlobale);
            var created = await CreateAsync(
                dto with
                {
                    Numero = numero,
                    TotalTtc = ttc,
                    Conditions = null,
                },
                cancellationToken);

            return (await GetDevisByIdAsync(created.Id, cancellationToken))!;
        });

    public Task UpdateDevisAsync(
        int id,
        UpdateDevisFournisseurDto dto,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(UpdateDevisAsync), async () =>
        {
            await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

            var (_, _, ttc) = DocumentTotals.Compute(
                dto.Lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)),
                dto.RemiseGlobale);
            var toUpdate = dto with { TotalTtc = ttc };
            await ValidateAsync(UpdateValidator, toUpdate, cancellationToken);

            var entity = await Repo.GetByIdWithNavigationsAsync(
                    id,
                    [d => d.Lignes],
                    cancellationToken)
                ?? throw new KeyNotFoundException($"Devis fournisseur {id} introuvable.");

            entity.Lignes.Clear();
            Mapper.Map(toUpdate, entity);

            await Repo.UpdateAsync(entity, cancellationToken);
        });

    public Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GenerateNumeroAsync), async () =>
        {
            var year = DateTime.Today.Year;
            var prefix = $"DEVF-{year}-";
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
