using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services.Client;

public interface IClientService : IGenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>
{
    Task<IReadOnlyList<TiersDto>> GetClientsAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default);
    Task<ClientCompteDto> GetCompteAsync(int clientId, CancellationToken cancellationToken = default);
}
