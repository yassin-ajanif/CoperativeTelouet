using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Domain.Entities.Stockage;

namespace CoperativeTelouet.Business.Services.Stockage;

public interface IChambreFroideService
    : IGenericService<ChambreFroide, ChambreFroideDto, CreateChambreFroideDto, UpdateChambreFroideDto>
{
    Task<IReadOnlyList<ChambreFroideDto>> GetChambresAsync(
        string? search = null,
        bool actifsSeulement = false,
        CancellationToken cancellationToken = default);

    Task<ChambreFroideDto?> GetChambreByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<ChambreFroideDto> CreateChambreAsync(
        CreateChambreFroideDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateChambreAsync(
        int id,
        UpdateChambreFroideDto dto,
        CancellationToken cancellationToken = default);

    Task DeleteChambreAsync(int id, CancellationToken cancellationToken = default);
}
