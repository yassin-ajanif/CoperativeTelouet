using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Services.Client.Avoir;

public interface IAvoirClientService
    : IGenericService<AvoirClient, AvoirClientDto, CreateAvoirClientDto, UpdateAvoirClientDto>
{
    Task<PagedResult<AvoirClientListItemDto>> GetAvoirsAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<AvoirClientDto?> GetAvoirByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<AvoirClientDto> CreateAvoirAsync(
        CreateAvoirClientDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateAvoirAsync(
        int id,
        UpdateAvoirClientDto dto,
        CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);
}
