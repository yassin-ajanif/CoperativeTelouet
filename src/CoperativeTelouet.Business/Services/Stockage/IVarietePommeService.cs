using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Domain.Entities.Stockage;

namespace CoperativeTelouet.Business.Services.Stockage;

public interface IVarietePommeService
    : IGenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>
{
    Task<IReadOnlyList<VarietePommeDto>> GetVarietesAsync(
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<VarietePommeDto?> GetVarieteByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<VarietePommeDto> CreateVarieteAsync(
        CreateVarietePommeDto dto,
        CancellationToken cancellationToken = default);

    Task UpdateVarieteAsync(
        int id,
        UpdateVarietePommeDto dto,
        CancellationToken cancellationToken = default);

    Task DeleteVarieteAsync(int id, CancellationToken cancellationToken = default);
}
