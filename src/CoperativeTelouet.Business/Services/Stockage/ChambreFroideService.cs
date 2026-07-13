using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities.Stockage;
using CoperativeTelouet.Domain.Logging;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Stockage;

public class ChambreFroideService
    : GenericService<ChambreFroide, ChambreFroideDto, CreateChambreFroideDto, UpdateChambreFroideDto>,
      IChambreFroideService
{
    public ChambreFroideService(
        IRepository<ChambreFroide> repo,
        IMapper mapper,
        IEnumerable<IValidator<CreateChambreFroideDto>> createValidators,
        IEnumerable<IValidator<UpdateChambreFroideDto>> updateValidators,
        IErrorLogger logger)
        : base(repo, mapper, createValidators, updateValidators, logger)
    {
    }

    public Task<IReadOnlyList<ChambreFroideDto>> GetChambresAsync(
        string? search = null,
        bool actifsSeulement = false,
        CancellationToken cancellationToken = default)
        => RunLoggedAsync(nameof(GetChambresAsync), async () =>
        {
            var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

            var list = await FindAsync(
                c => (!actifsSeulement || c.Actif)
                     && (q == null || c.Nom.Contains(q)),
                cancellationToken);

            return (IReadOnlyList<ChambreFroideDto>)list
                .OrderByDescending(c => c.Actif)
                .ThenBy(c => c.Nom)
                .ToList();
        });

    public Task<ChambreFroideDto?> GetChambreByIdAsync(int id, CancellationToken cancellationToken = default)
        => GetByIdAsync(id, cancellationToken);

    public Task<ChambreFroideDto> CreateChambreAsync(
        CreateChambreFroideDto dto,
        CancellationToken cancellationToken = default)
        => CreateAsync(
            dto with { Nom = dto.Nom.Trim() },
            cancellationToken);

    public Task UpdateChambreAsync(
        int id,
        UpdateChambreFroideDto dto,
        CancellationToken cancellationToken = default)
        => UpdateAsync(
            id,
            dto with { Nom = dto.Nom.Trim() },
            cancellationToken);

    public Task DeleteChambreAsync(int id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);
}
