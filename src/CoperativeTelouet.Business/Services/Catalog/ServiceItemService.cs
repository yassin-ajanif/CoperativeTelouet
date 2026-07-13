using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Logging;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Catalog;

public class ServiceItemService
    : GenericService<ServiceItem, ServiceItemDto, CreateServiceItemDto, UpdateServiceItemDto>,
      IServiceItemService
{
    public ServiceItemService(
        IRepository<ServiceItem> repo,
        IMapper mapper,
        IEnumerable<IValidator<CreateServiceItemDto>> createValidators,
        IEnumerable<IValidator<UpdateServiceItemDto>> updateValidators,
        IErrorLogger logger)
        : base(repo, mapper, createValidators, updateValidators, logger)
    {
    }

    public Task<PagedResult<ServiceItemDto>> GetServicesAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
    {
        var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

        return QueryPagedAsync(
            s => q == null
                 || s.Reference.Contains(q)
                 || s.Designation.Contains(q),
            query => query.OrderBy(s => s.Reference),
            s => new ServiceItemDto(
                s.Id,
                s.Reference,
                s.Designation,
                s.Unite,
                s.PrixVenteHT,
                s.CoutHT,
                s.TauxTVA,
                s.Actif,
                s.Note,
                s.ImageData),
            page,
            pageSize,
            cancellationToken);
    }

    public Task<ServiceItemDto?> GetServiceByIdAsync(int id, CancellationToken cancellationToken = default)
        => GetByIdAsync(id, cancellationToken);

    public Task<ServiceItemDto> CreateServiceAsync(
        CreateServiceItemDto dto,
        CancellationToken cancellationToken = default)
        => CreateAsync(
            dto with
            {
                Reference = dto.Reference.Trim(),
                Designation = dto.Designation.Trim(),
                Unite = string.IsNullOrWhiteSpace(dto.Unite) ? null : dto.Unite.Trim(),
                Note = string.IsNullOrWhiteSpace(dto.Note) ? null : dto.Note.Trim(),
            },
            cancellationToken);

    public Task UpdateServiceAsync(
        int id,
        UpdateServiceItemDto dto,
        CancellationToken cancellationToken = default)
        => UpdateAsync(
            id,
            dto with
            {
                Reference = dto.Reference.Trim(),
                Designation = dto.Designation.Trim(),
                Unite = string.IsNullOrWhiteSpace(dto.Unite) ? null : dto.Unite.Trim(),
                Note = string.IsNullOrWhiteSpace(dto.Note) ? null : dto.Note.Trim(),
            },
            cancellationToken);

    public Task DeleteServiceAsync(int id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);
}
