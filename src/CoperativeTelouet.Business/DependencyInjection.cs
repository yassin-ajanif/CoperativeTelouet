using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Mapping;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.Services.Stockage;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Stockage;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Registers AutoMapper (scanning the Business assembly for profiles) and the
    /// business services: closed generics for simple catalog entities plus the
    /// concrete services that carry custom logic (e.g. StockageService).
    /// </summary>
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CatalogProfile).Assembly);

        // simple catalog entities: register the closed generic service (no subclass needed)
        services.AddScoped<
            IGenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>,
            GenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>>();

        services.AddScoped<
            IGenericService<Categorie, CategorieDto, CreateCategorieDto, UpdateCategorieDto>,
            GenericService<Categorie, CategorieDto, CreateCategorieDto, UpdateCategorieDto>>();

        services.AddScoped<
            IGenericService<ChambreFroide, ChambreFroideDto, CreateChambreFroideDto, UpdateChambreFroideDto>,
            GenericService<ChambreFroide, ChambreFroideDto, CreateChambreFroideDto, UpdateChambreFroideDto>>();

        services.AddScoped<
            IGenericService<TypeCharge, TypeChargeDto, CreateTypeChargeDto, UpdateTypeChargeDto>,
            GenericService<TypeCharge, TypeChargeDto, CreateTypeChargeDto, UpdateTypeChargeDto>>();

        services.AddScoped<
            IGenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>,
            GenericService<Tiers, TiersDto, CreateTiersDto, UpdateTiersDto>>();

        services.AddScoped<
            IGenericService<Produit, ProduitDto, CreateProduitDto, UpdateProduitDto>,
            GenericService<Produit, ProduitDto, CreateProduitDto, UpdateProduitDto>>();

        services.AddScoped<
            IGenericService<ServiceItem, ServiceItemDto, CreateServiceItemDto, UpdateServiceItemDto>,
            GenericService<ServiceItem, ServiceItemDto, CreateServiceItemDto, UpdateServiceItemDto>>();

        services.AddScoped<
            IGenericService<Charge, ChargeDto, CreateChargeDto, UpdateChargeDto>,
            GenericService<Charge, ChargeDto, CreateChargeDto, UpdateChargeDto>>();

        // entities with custom logic: register the concrete service
        services.AddScoped<StockageService>();

        return services;
    }
}
