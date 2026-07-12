using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Services.Client.Devis;

public interface IDevisClientService
    : IGenericService<DevisClient, DevisClientDto, CreateDevisClientDto, UpdateDevisClientDto>
{
    Task<PagedResult<DevisClientListItemDto>> GetDevisAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<DevisClientDto?> GetDevisByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<DevisClientDto> CreateDevisAsync(CreateDevisClientDto dto, CancellationToken cancellationToken = default);

    Task UpdateDevisAsync(int id, UpdateDevisClientDto dto, CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);

    static decimal LineHt(decimal quantite, decimal prixUnitaireHt, decimal remise) =>
        DocumentTotals.LineHt(quantite, prixUnitaireHt, remise);

    static decimal LineTtc(decimal quantite, decimal prixUnitaireHt, decimal remise, decimal tauxTva) =>
        DocumentTotals.LineTtc(quantite, prixUnitaireHt, remise, tauxTva);

    static (decimal Ht, decimal Tva, decimal Ttc) ComputeTotals(
        IEnumerable<CreateDevisClientLigneDto> lignes,
        decimal remiseGlobale) =>
        DocumentTotals.Compute(
            lignes.Select(l => (l.Quantite, l.PrixUnitaireHT, l.Remise, l.TauxTVA)),
            remiseGlobale);
}
