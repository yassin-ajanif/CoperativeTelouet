using AutoMapper;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Common;
using FluentValidation;

namespace CoperativeTelouet.Business.Services;

public class GenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
{
    protected readonly IRepository<TEntity> Repo;
    protected readonly IMapper Mapper;
    protected readonly IValidator<TCreateDto>? CreateValidator;
    protected readonly IValidator<TUpdateDto>? UpdateValidator;

    public GenericService(
        IRepository<TEntity> repo,
        IMapper mapper,
        IEnumerable<IValidator<TCreateDto>> createValidators,
        IEnumerable<IValidator<TUpdateDto>> updateValidators)
    {
        Repo = repo;
        Mapper = mapper;
        CreateValidator = createValidators.FirstOrDefault();
        UpdateValidator = updateValidators.FirstOrDefault();
    }

    public async Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Repo.GetByIdAsync(id, cancellationToken);
        return entity is null ? default : Mapper.Map<TDto>(entity);
    }

    public async Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => Mapper.Map<IReadOnlyList<TDto>>(await Repo.GetAllAsync(cancellationToken));

    public async Task<TDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default)
    {
        await ValidateAsync(CreateValidator, dto, cancellationToken);
        return Mapper.Map<TDto>(await Repo.AddAsync(Mapper.Map<TEntity>(dto), cancellationToken));
    }

    public async Task UpdateAsync(int id, TUpdateDto dto, CancellationToken cancellationToken = default)
    {
        await ValidateAsync(UpdateValidator, dto, cancellationToken);
        var entity = await Repo.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException();
        Mapper.Map(dto, entity);
        await Repo.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        => Repo.DeleteAsync(id, cancellationToken);

    protected static async Task ValidateAsync<T>(
        IValidator<T>? validator,
        T instance,
        CancellationToken cancellationToken)
    {
        if (validator is null)
            return;

        await validator.ValidateAndThrowAsync(instance, cancellationToken);
    }
}
