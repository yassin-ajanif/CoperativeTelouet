using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Services.Client.BonLivraison;

public interface IBonLivraisonClientService
    : IGenericService<BonLivraisonClient, BonLivraisonClientDto, CreateBonLivraisonClientDto, UpdateBonLivraisonClientDto>
{
    Task<PagedResult<BonLivraisonClientListItemDto>> GetBonsLivraisonAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<BonLivraisonClientDto?> GetBonLivraisonByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BonLivraisonClientDto> CreateBonLivraisonAsync(
        CreateBonLivraisonClientDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateBonLivraisonAsync(
        int id,
        UpdateBonLivraisonClientDto dto,
        CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);
}
