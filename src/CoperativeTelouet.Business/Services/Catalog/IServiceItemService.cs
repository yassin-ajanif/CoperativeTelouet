using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services.Catalog;

public interface IServiceItemService
    : IGenericService<ServiceItem, ServiceItemDto, CreateServiceItemDto, UpdateServiceItemDto>
{
    Task<PagedResult<ServiceItemDto>> GetServicesAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<ServiceItemDto?> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<ServiceItemDto> CreateServiceAsync(
        CreateServiceItemDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateServiceAsync(
        int id,
        UpdateServiceItemDto dto,
        CancellationToken cancellationToken = default);

    Task DeleteServiceAsync(int id, CancellationToken cancellationToken = default);
}
