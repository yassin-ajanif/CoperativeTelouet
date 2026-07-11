using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Services.Fournisseur;

public interface IFournisseurService
{
    Task<IReadOnlyList<TiersDto>> GetFournisseursAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<TiersDto?> GetFournisseurByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TiersDto> CreateFournisseurAsync(CreateTiersDto dto, CancellationToken cancellationToken = default);
    Task UpdateFournisseurAsync(int id, UpdateTiersDto dto, CancellationToken cancellationToken = default);
    Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default);
    Task<FournisseurCompteDto> GetCompteAsync(int fournisseurId, CancellationToken cancellationToken = default);
}
