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

namespace CoperativeTelouet.Business.Services.Fournisseur.Facture;

public class FactureFournisseurService
    : GenericService<FactureFournisseur, FactureFournisseurDto, CreateFactureFournisseurDto, UpdateFactureFournisseurDto>,
      IFactureFournisseurService
{
    private readonly IRepository<FactureFournisseurLigne> _lignes;
    private readonly IRepository<Tiers> _tiers;
    private readonly IArticleSuggestionService _articles;

    public FactureFournisseurService(
        IRepository<FactureFournisseur> factures,
        IRepository<FactureFournisseurLigne> lignes,
        IRepository<Tiers> tiers,
        IArticleSuggestionService articles,
        IMapper mapper,
        IEnumerable<IValidator<CreateFactureFournisseurDto>> createValidators,
        IEnumerable<IValidator<UpdateFactureFournisseurDto>> updateValidators,
        IErrorLogger logger)
        : base(factures, mapper, createValidators, updateValidators, logger)
    {
        _lignes = lignes;
        _tiers = tiers;
        _articles = articles;
    }

    public Task<PagedResult<FactureFournisseurListItemDto>> GetFacturesAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetFacturesAsync), () =>
        {
            var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();
            var from = dateFrom?.Date;
            var toExclusive = dateTo?.Date.AddDays(1);

            return QueryPagedAsync(
                f => (from == null || f.Date >= from)
                     && (toExclusive == null || f.Date < toExclusive)
                     && (q == null
                         || f.Numero.Contains(q)
                         || f.Fournisseur.Nom.Contains(q)),
                query => query.OrderByDescending(f => f.Date).ThenByDescending(f => f.Id),
                f => new FactureFournisseurListItemDto(
                    f.Id,
                    f.Numero,
                    f.FournisseurId,
                    f.Fournisseur.Nom,
                    f.Date,
                    f.DateEcheance,
                    f.EstPayee,
                    f.TotalTtc,
                    f.Note),
                page,
                pageSize,
                cancellationToken);
        });

    public Task<FactureFournisseurDto?> GetFactureByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetFactureByIdAsync), async () =>
        {
            var dto = await GetByIdAsync(id, cancellationToken);
            if (dto is null)
                return null;

            var lignes = await _lignes.FindAsync(l => l.FactureFournisseurId == id, cancellationToken);
            return dto with
            {
                Lignes = lignes.Select(l => Mapper.Map<FactureFournisseurLigneDto>(l)).ToList(),
            };
        });

    public Task<FactureFournisseurDto> CreateFactureAsync(
        CreateFactureFournisseurDto dto,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(CreateFactureAsync), async () =>
        {
            await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

            var numero = string.IsNullOrWhiteSpace(dto.Numero)
                ? await GenerateNumeroAsync(cancellationToken)
                : dto.Numero.Trim();

            var (_, _, ttc) = DocumentTotals.Compute(
                dto.Lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)),
                dto.RemiseGlobale);
            var created = await CreateAsync(
                dto with { Numero = numero, TotalTtc = ttc },
                cancellationToken);

            return (await GetFactureByIdAsync(created.Id, cancellationToken))!;
        });

    public Task UpdateFactureAsync(
        int id,
        UpdateFactureFournisseurDto dto,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(UpdateFactureAsync), async () =>
        {
            await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

            var (_, _, ttc) = DocumentTotals.Compute(
                dto.Lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)),
                dto.RemiseGlobale);
            var toUpdate = dto with { TotalTtc = ttc };
            await ValidateAsync(UpdateValidator, toUpdate, cancellationToken);

            var entity = await Repo.GetByIdWithNavigationsAsync(
                    id,
                    [f => f.Lignes],
                    cancellationToken)
                ?? throw new KeyNotFoundException($"Facture fournisseur {id} introuvable.");

            entity.Lignes.Clear();
            Mapper.Map(toUpdate, entity);

            await Repo.UpdateAsync(entity, cancellationToken);
        });

    public Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GenerateNumeroAsync), async () =>
        {
            var year = DateTime.Today.Year;
            var prefix = $"FACF-{year}-";
            var existing = await FindAsync(f => f.Numero.StartsWith(prefix), cancellationToken);
            var next = existing
                .Select(f =>
                {
                    var tail = f.Numero[prefix.Length..];
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
