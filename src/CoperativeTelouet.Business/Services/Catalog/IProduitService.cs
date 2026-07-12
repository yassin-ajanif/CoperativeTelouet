using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Services.Catalog;

public interface IProduitService
    : CoperativeTelouet.Business.Services.IGenericService<Produit, ProduitDto, CreateProduitDto, UpdateProduitDto>
{
    Task<PagedResult<ProduitDto>> GetProduitsAsync(
        string? search = null,
        int page = 1,
        int pageSize = 15,
        CancellationToken cancellationToken = default);

    Task<ProduitDto> CreateProduitAsync(CreateProduitDto dto, CancellationToken cancellationToken = default);
    Task UpdateProduitAsync(int id, UpdateProduitDto dto, CancellationToken cancellationToken = default);
    Task DeleteProduitAsync(int id, CancellationToken cancellationToken = default);
}
