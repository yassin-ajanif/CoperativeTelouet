using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Business.Services;

public interface IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
{
    Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, TUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
