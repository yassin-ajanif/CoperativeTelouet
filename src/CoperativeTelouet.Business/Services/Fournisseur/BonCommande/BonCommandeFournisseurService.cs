using CoperativeTelouet.Business.Services;
using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Fournisseur;
using CoperativeTelouet.Domain.Enums;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Fournisseur.BonCommande;

public class BonCommandeFournisseurService
    : GenericService<BonCommandeFournisseur, BonCommandeFournisseurDto, CreateBonCommandeFournisseurDto, UpdateBonCommandeFournisseurDto>,
      IBonCommandeFournisseurService
{
    private readonly IRepository<BonCommandeFournisseurLigne> _lignes;
    private readonly IRepository<Tiers> _tiers;
    private readonly IArticleSuggestionService _articles;

    public BonCommandeFournisseurService(
        IRepository<BonCommandeFournisseur> bons,
        IRepository<BonCommandeFournisseurLigne> lignes,
        IRepository<Tiers> tiers,
        IArticleSuggestionService articles,
        IMapper mapper,
        IEnumerable<IValidator<CreateBonCommandeFournisseurDto>> createValidators,
        IEnumerable<IValidator<UpdateBonCommandeFournisseurDto>> updateValidators)
        : base(bons, mapper, createValidators, updateValidators)
    {
        _lignes = lignes;
        _tiers = tiers;
        _articles = articles;
    }

    public Task<PagedResult<BonCommandeFournisseurListItemDto>> GetBonsCommandeAsync(
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
            b => (from == null || b.Date >= from)
                 && (toExclusive == null || b.Date < toExclusive)
                 && (q == null
                     || b.Numero.Contains(q)
                     || b.Fournisseur.Nom.Contains(q)),
            query => query.OrderByDescending(b => b.Date).ThenByDescending(b => b.Id),
            b => new BonCommandeFournisseurListItemDto(
                b.Id,
                b.Numero,
                b.FournisseurId,
                b.Fournisseur.Nom,
                b.DevisFournisseurId,
                b.Date,
                b.TotalTtc,
                b.Note),
            page,
            pageSize,
            cancellationToken);
    }

    public async Task<BonCommandeFournisseurDto?> GetBonCommandeByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var dto = await GetByIdAsync(id, cancellationToken);
        if (dto is null)
            return null;

        var lignes = await _lignes.FindAsync(l => l.BonCommandeFournisseurId == id, cancellationToken);
        return dto with
        {
            Lignes = lignes.Select(l => Mapper.Map<BonCommandeFournisseurLigneDto>(l)).ToList(),
        };
    }

    public async Task<BonCommandeFournisseurDto> CreateBonCommandeAsync(
        CreateBonCommandeFournisseurDto dto,
        CancellationToken cancellationToken = default)
    {
        await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

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
        UpdateBonCommandeFournisseurDto dto,
        CancellationToken cancellationToken = default)
    {
        await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

        var (_, _, ttc) = DocumentTotals.Compute(
            dto.Lignes.Select(l => (l.QuantiteCommandee, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));
        var toUpdate = dto with { TotalTtc = ttc };
        await ValidateAsync(UpdateValidator, toUpdate, cancellationToken);

        var entity = await Repo.GetByIdWithNavigationsAsync(
                id,
                [b => b.Lignes],
                cancellationToken)
            ?? throw new KeyNotFoundException($"Bon de commande fournisseur {id} introuvable.");

        entity.Lignes.Clear();
        Mapper.Map(toUpdate, entity);

        await Repo.UpdateAsync(entity, cancellationToken);
    }

    public async Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default)
    {
        var year = DateTime.Today.Year;
        var prefix = $"BCF-{year}-";
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
    }

    public Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default)
        => _articles.SearchArticlesAsync(search, cancellationToken);

    private async Task EnsureFournisseurExistsAsync(int fournisseurId, CancellationToken cancellationToken)
    {
        var fournisseur = await _tiers.GetByIdAsync(fournisseurId, cancellationToken)
            ?? throw new KeyNotFoundException($"Fournisseur {fournisseurId} introuvable.");

        if (fournisseur.Type is not (TypeTiers.Fournisseur or TypeTiers.LesDeux))
            throw new InvalidOperationException("Le tiers sélectionné n'est pas un fournisseur.");
    }
}
