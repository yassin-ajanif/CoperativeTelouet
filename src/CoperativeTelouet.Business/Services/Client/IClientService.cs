using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services.Client;

public interface IClientService : IGenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>
{
    Task<PagedResult<TiersDto>> GetClientsAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<TiersDto?> GetClientByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TiersDto> CreateClientAsync(CreateTiersDto dto, CancellationToken cancellationToken = default);
    Task UpdateClientAsync(int id, UpdateTiersDto dto, CancellationToken cancellationToken = default);
    Task DeleteClientAsync(int id, CancellationToken cancellationToken = default);

    Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default);
    Task<ClientCompteDto> GetCompteAsync(int clientId, CancellationToken cancellationToken = default);
}
