using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Services.Client;

public interface IClientService
{
    Task<IReadOnlyList<TiersDto>> GetClientsAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<TiersDto?> GetClientByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TiersDto> CreateClientAsync(CreateTiersDto dto, CancellationToken cancellationToken = default);
    Task UpdateClientAsync(int id, UpdateTiersDto dto, CancellationToken cancellationToken = default);
    Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default);
    Task<ClientCompteDto> GetCompteAsync(int clientId, CancellationToken cancellationToken = default);
}
