using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services.Fournisseur;

public interface IFournisseurService : IGenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>
{
    Task<PagedResult<TiersDto>> GetFournisseursAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<TiersDto?> GetFournisseurByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TiersDto> CreateFournisseurAsync(CreateTiersDto dto, CancellationToken cancellationToken = default);
    Task UpdateFournisseurAsync(int id, UpdateTiersDto dto, CancellationToken cancellationToken = default);
    Task DeleteFournisseurAsync(int id, CancellationToken cancellationToken = default);

    Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default);
    Task<FournisseurCompteDto> GetCompteAsync(int fournisseurId, CancellationToken cancellationToken = default);
}
