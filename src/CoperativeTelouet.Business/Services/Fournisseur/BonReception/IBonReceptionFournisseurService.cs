using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Business.Services.Fournisseur.BonReception;

public interface IBonReceptionFournisseurService
    : IGenericService<BonReceptionFournisseur, BonReceptionFournisseurDto, CreateBonReceptionFournisseurDto, UpdateBonReceptionFournisseurDto>
{
    Task<PagedResult<BonReceptionFournisseurListItemDto>> GetBonsReceptionAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<BonReceptionFournisseurDto?> GetBonReceptionByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BonReceptionFournisseurDto> CreateBonReceptionAsync(
        CreateBonReceptionFournisseurDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateBonReceptionAsync(
        int id,
        UpdateBonReceptionFournisseurDto dto,
        CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);

    static decimal LineHt(decimal quantite, decimal prixUnitaireHt) =>
        DocumentTotals.LineHt(quantite, prixUnitaireHt, 0);

    static decimal LineTtc(decimal quantite, decimal prixUnitaireHt, decimal tauxTva) =>
        DocumentTotals.LineTtc(quantite, prixUnitaireHt, 0, tauxTva);

    static (decimal Ht, decimal Tva, decimal Ttc) ComputeTotals(
        IEnumerable<CreateBonReceptionFournisseurLigneDto> lignes) =>
        DocumentTotals.Compute(
            lignes.Select(l => (l.QuantiteRecue, l.PrixUnitaireHT, 0m, l.TauxTVA)));
}
