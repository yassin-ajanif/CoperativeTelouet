using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Services.Client.Facture;

public interface IFactureClientService
    : IGenericService<FactureClient, FactureClientDto, CreateFactureClientDto, UpdateFactureClientDto>
{
    Task<PagedResult<FactureClientListItemDto>> GetFacturesAsync(
        string? search = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<FactureClientDto?> GetFactureByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<FactureClientDto> CreateFactureAsync(
        CreateFactureClientDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateFactureAsync(
        int id,
        UpdateFactureClientDto dto,
        CancellationToken cancellationToken = default);

    Task<string> GenerateNumeroAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ArticleSuggestionDto>> SearchArticlesAsync(
        string? search,
        CancellationToken cancellationToken = default);
}
