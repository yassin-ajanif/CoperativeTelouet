using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services.Fournisseur;

public interface IFournisseurService : IGenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>
{
    Task<IReadOnlyList<TiersDto>> GetFournisseursAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<TiersDto> ToggleActifAsync(int id, CancellationToken cancellationToken = default);
    Task<FournisseurCompteDto> GetCompteAsync(int fournisseurId, CancellationToken cancellationToken = default);
}
