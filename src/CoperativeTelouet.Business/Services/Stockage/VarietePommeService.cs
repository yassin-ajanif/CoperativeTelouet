using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities.Stockage;
using CoperativeTelouet.Domain.Logging;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Stockage;

public class VarietePommeService
    : GenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>,
      IVarietePommeService
{
    public VarietePommeService(
        IRepository<VarietePomme> repo,
        IMapper mapper,
        IEnumerable<IValidator<CreateVarietePommeDto>> createValidators,
        IEnumerable<IValidator<UpdateVarietePommeDto>> updateValidators,
        IErrorLogger logger)
        : base(repo, mapper, createValidators, updateValidators, logger)
    {
    }

    public Task<IReadOnlyList<VarietePommeDto>> GetVarietesAsync(
        string? search = null,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetVarietesAsync), async () =>
        {
            var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

            var list = await FindAsync(
                v => q == null || v.Nom.Contains(q),
                cancellationToken);

            return (IReadOnlyList<VarietePommeDto>)list
                .OrderBy(v => v.Nom)
                .ToList();
        });

    public Task<VarietePommeDto?> GetVarieteByIdAsync(int id, CancellationToken cancellationToken = default)
        => GetByIdAsync(id, cancellationToken);

    public Task<VarietePommeDto> CreateVarieteAsync(
        CreateVarietePommeDto dto,
        CancellationToken cancellationToken = default)
        => CreateAsync(
            dto with { Nom = dto.Nom.Trim() },
            cancellationToken);

    public Task UpdateVarieteAsync(
        int id,
        UpdateVarietePommeDto dto,
        CancellationToken cancellationToken = default)
        => UpdateAsync(
            id,
            dto with { Nom = dto.Nom.Trim() },
            cancellationToken);

    public Task DeleteVarieteAsync(int id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);
}
