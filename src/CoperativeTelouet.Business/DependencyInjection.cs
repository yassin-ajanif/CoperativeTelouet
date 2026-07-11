using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Business.Mapping;
using CoperativeTelouet.Business.Services;
using CoperativeTelouet.Business.Services.Client;
using CoperativeTelouet.Business.Services.Stockage;
using CoperativeTelouet.Domain.Entities;
using CoperativeTelouet.Domain.Entities.Stockage;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Registers AutoMapper, FluentValidation (per-DTO validators), and business services.
    /// </summary>
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CatalogProfile).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

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
        services.AddScoped<IClientService, ClientService>();

        return services;
    }
}
