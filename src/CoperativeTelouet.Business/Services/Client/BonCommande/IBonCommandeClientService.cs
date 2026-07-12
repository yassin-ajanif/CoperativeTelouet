using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Services.Client.BonCommande;

public interface IBonCommandeClientService
    : IGenericService<BonCommandeClient, BonCommandeClientDto, CreateBonCommandeClientDto, UpdateBonCommandeClientDto>
{
    Task<PagedResult<BonCommandeClientListItemDto>> GetBonsCommandeAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<BonCommandeClientDto?> GetBonCommandeByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BonCommandeClientDto> CreateBonCommandeAsync(
        CreateBonCommandeClientDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateBonCommandeAsync(
        int id,
        UpdateBonCommandeClientDto dto,
        CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);
}
