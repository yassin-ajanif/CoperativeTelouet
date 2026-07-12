using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Business.Services.Fournisseur.Avoir;

public interface IAvoirFournisseurService
    : IGenericService<AvoirFournisseur, AvoirFournisseurDto, CreateAvoirFournisseurDto, UpdateAvoirFournisseurDto>
{
    Task<PagedResult<AvoirFournisseurListItemDto>> GetAvoirsAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<AvoirFournisseurDto?> GetAvoirByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<AvoirFournisseurDto> CreateAvoirAsync(
        CreateAvoirFournisseurDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateAvoirAsync(
        int id,
        UpdateAvoirFournisseurDto dto,
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
        IEnumerable<CreateAvoirFournisseurLigneDto> lignes) =>
        DocumentTotals.Compute(
            lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)));
}
