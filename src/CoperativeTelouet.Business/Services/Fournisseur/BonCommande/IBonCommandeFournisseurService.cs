using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Business.Services.Fournisseur.BonCommande;

public interface IBonCommandeFournisseurService
    : IGenericService<BonCommandeFournisseur, BonCommandeFournisseurDto, CreateBonCommandeFournisseurDto, UpdateBonCommandeFournisseurDto>
{
    Task<PagedResult<BonCommandeFournisseurListItemDto>> GetBonsCommandeAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<BonCommandeFournisseurDto?> GetBonCommandeByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BonCommandeFournisseurDto> CreateBonCommandeAsync(
        CreateBonCommandeFournisseurDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateBonCommandeAsync(
        int id,
        UpdateBonCommandeFournisseurDto dto,
        CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);

    static decimal LineHt(decimal quantite, decimal prixUnitaireHt, decimal remise) =>
        DocumentTotals.LineHt(quantite, prixUnitaireHt, remise);

    static decimal LineTtc(decimal quantite, decimal prixUnitaireHt, decimal remise, decimal tauxTva) =>
        DocumentTotals.LineTtc(quantite, prixUnitaireHt, remise, tauxTva);

    static (decimal Ht, decimal Tva, decimal Ttc) ComputeTotals(
        IEnumerable<CreateBonCommandeFournisseurLigneDto> lignes) =>
        DocumentTotals.Compute(
            lignes.Select(l => (l.QuantiteCommandee, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));
}
