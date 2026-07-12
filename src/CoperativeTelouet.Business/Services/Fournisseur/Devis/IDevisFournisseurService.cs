using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Business.Services.Fournisseur.Devis;

public interface IDevisFournisseurService
    : IGenericService<DevisFournisseur, DevisFournisseurDto, CreateDevisFournisseurDto, UpdateDevisFournisseurDto>
{
    Task<PagedResult<DevisFournisseurListItemDto>> GetDevisAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<DevisFournisseurDto?> GetDevisByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<DevisFournisseurDto> CreateDevisAsync(CreateDevisFournisseurDto dto, CancellationToken cancellationToken = default);

    Task UpdateDevisAsync(int id, UpdateDevisFournisseurDto dto, CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);

    static decimal LineHt(decimal quantite, decimal prixUnitaireHt, decimal remise) =>
        DocumentTotals.LineHt(quantite, prixUnitaireHt, remise);

    static decimal LineTtc(decimal quantite, decimal prixUnitaireHt, decimal remise, decimal tauxTva) =>
        DocumentTotals.LineTtc(quantite, prixUnitaireHt, remise, tauxTva);

    static (decimal Ht, decimal Tva, decimal Ttc) ComputeTotals(
        IEnumerable<CreateDevisFournisseurLigneDto> lignes,
        decimal remiseGlobale) =>
        DocumentTotals.Compute(
            lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)),
            remiseGlobale);
}
