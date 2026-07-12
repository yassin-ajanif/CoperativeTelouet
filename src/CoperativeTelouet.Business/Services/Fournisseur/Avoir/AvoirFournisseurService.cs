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

namespace CoperativeTelouet.Business.Services.Fournisseur.Avoir;

public class AvoirFournisseurService
    : GenericService<AvoirFournisseur, AvoirFournisseurDto, CreateAvoirFournisseurDto, UpdateAvoirFournisseurDto>,
      IAvoirFournisseurService
{
    private readonly IRepository<AvoirFournisseurLigne> _lignes;
    private readonly IRepository<Tiers> _tiers;
    private readonly IArticleSuggestionService _articles;

    public AvoirFournisseurService(
        IRepository<AvoirFournisseur> avoirs,
        IRepository<AvoirFournisseurLigne> lignes,
        IRepository<Tiers> tiers,
        IArticleSuggestionService articles,
        IMapper mapper,
        IEnumerable<IValidator<CreateAvoirFournisseurDto>> createValidators,
        IEnumerable<IValidator<UpdateAvoirFournisseurDto>> updateValidators)
        : base(avoirs, mapper, createValidators, updateValidators)
    {
        _lignes = lignes;
        _tiers = tiers;
        _articles = articles;
    }

    public Task<PagedResult<AvoirFournisseurListItemDto>> GetAvoirsAsync(
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
            a => (from == null || a.Date >= from)
                 && (toExclusive == null || a.Date < toExclusive)
                 && (q == null
                     || a.Numero.Contains(q)
                     || a.Fournisseur.Nom.Contains(q)),
            query => query.OrderByDescending(a => a.Date).ThenByDescending(a => a.Id),
            a => new AvoirFournisseurListItemDto(
                a.Id,
                a.Numero,
                a.FournisseurId,
                a.Fournisseur.Nom,
                a.FactureFournisseurId,
                a.Date,
                a.TotalTtc,
                a.Motif),
            page,
            pageSize,
            cancellationToken);
    }

    public async Task<AvoirFournisseurDto?> GetAvoirByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var dto = await GetByIdAsync(id, cancellationToken);
        if (dto is null)
            return null;

        var lignes = await _lignes.FindAsync(l => l.AvoirFournisseurId == id, cancellationToken);
        return dto with
        {
            Lignes = lignes.Select(l => Mapper.Map<AvoirFournisseurLigneDto>(l)).ToList(),
        };
    }

    public async Task<AvoirFournisseurDto> CreateAvoirAsync(
        CreateAvoirFournisseurDto dto,
        CancellationToken cancellationToken = default)
    {
        await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

        var numero = string.IsNullOrWhiteSpace(dto.Numero)
            ? await GenerateNumeroAsync(cancellationToken)
            : dto.Numero.Trim();

        var (_, _, ttc) = DocumentTotals.Compute(
            dto.Lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));
        var created = await CreateAsync(
            dto with { Numero = numero, TotalTtc = ttc },
            cancellationToken);

        return (await GetAvoirByIdAsync(created.Id, cancellationToken))!;
    }

    public async Task UpdateAvoirAsync(
        int id,
        UpdateAvoirFournisseurDto dto,
        CancellationToken cancellationToken = default)
    {
        await EnsureFournisseurExistsAsync(dto.FournisseurId, cancellationToken);

        var (_, _, ttc) = DocumentTotals.Compute(
            dto.Lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));
        var toUpdate = dto with { TotalTtc = ttc };
        await ValidateAsync(UpdateValidator, toUpdate, cancellationToken);

        var entity = await Repo.GetByIdWithNavigationsAsync(
                id,
                [a => a.Lignes],
                cancellationToken)
            ?? throw new KeyNotFoundException($"Avoir fournisseur {id} introuvable.");

        entity.Lignes.Clear();
        Mapper.Map(toUpdate, entity);

        await Repo.UpdateAsync(entity, cancellationToken);
    }

    public async Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default)
    {
        var year = DateTime.Today.Year;
        var prefix = $"AVF-{year}-";
        var existing = await FindAsync(a => a.Numero.StartsWith(prefix), cancellationToken);
        var next = existing
            .Select(a =>
            {
                var tail = a.Numero[prefix.Length..];
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
