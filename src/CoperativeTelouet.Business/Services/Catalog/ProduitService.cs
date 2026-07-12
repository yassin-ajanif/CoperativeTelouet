using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Logging;
using CoperativeTelouet.Domain.Entities;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Catalog;

public class ProduitService
    : CoperativeTelouet.Business.Services.GenericService<Produit, ProduitDto, CreateProduitDto, UpdateProduitDto>,
      IProduitService
{
    public ProduitService(
        IRepository<Produit> produits,
        IMapper mapper,
        IEnumerable<IValidator<CreateProduitDto>> createValidators,
        IEnumerable<IValidator<UpdateProduitDto>> updateValidators,
        IErrorLogger logger)
        : base(produits, mapper, createValidators, updateValidators, logger)
    {
    }

    public Task<PagedResult<ProduitDto>> GetProduitsAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default)
    {
        var q = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

        return QueryPagedAsync(
            p => q == null
                 || p.Reference.Contains(q)
                 || p.Designation.Contains(q)
                 || (p.CodeBarre != null && p.CodeBarre.Contains(q)),
            query => query.OrderBy(p => p.Reference),
            p => new ProduitDto(
                p.Id,
                p.Reference,
                p.CodeBarre,
                p.Designation,
                p.Unite,
                p.PrixAchatHT,
                p.PrixVenteHT,
                p.TauxTVA,
                p.StockActuel,
                p.StockMinimum,
                p.CategorieId,
                p.Actif,
                p.ImageData),
            page,
            pageSize,
            cancellationToken);
    }

    public Task<ProduitDto> CreateProduitAsync(
        CreateProduitDto dto,
        CancellationToken cancellationToken = default)
        => CreateAsync(dto with { StockActuel = 0 }, cancellationToken);

    public async Task UpdateProduitAsync(
        int id,
        UpdateProduitDto dto,
        CancellationToken cancellationToken = default)
    {
        var existing = await GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Produit {id} introuvable.");

        // Stock is managed by stock movements, not the fiche form.
        await UpdateAsync(id, dto with { StockActuel = existing.StockActuel }, cancellationToken);
    }

    public Task DeleteProduitAsync(int id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);
}
