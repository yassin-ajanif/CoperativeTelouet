using AutoMapper;
using CoperativeTelouet.Business.DTOs;
using CoperativeTelouet.Domain.Entities;

namespace CoperativeTelouet.Business.Mapping;

public class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        CreateMap<Tiers, TiersDto>();
        CreateMap<TiersDto, UpdateTiersDto>();
        CreateMap<CreateTiersDto, Tiers>();
        CreateMap<UpdateTiersDto, Tiers>();

        CreateMap<Categorie, CategorieDto>();
        CreateMap<CreateCategorieDto, Categorie>();
        CreateMap<UpdateCategorieDto, Categorie>();

        CreateMap<Produit, ProduitDto>();
        CreateMap<CreateProduitDto, Produit>();
        CreateMap<UpdateProduitDto, Produit>();

        CreateMap<ServiceItem, ServiceItemDto>();
        CreateMap<CreateServiceItemDto, ServiceItem>();
        CreateMap<UpdateServiceItemDto, ServiceItem>();

        CreateMap<TypeCharge, TypeChargeDto>();
        CreateMap<CreateTypeChargeDto, TypeCharge>();
        CreateMap<UpdateTypeChargeDto, TypeCharge>();

        CreateMap<Charge, ChargeDto>();
        CreateMap<CreateChargeDto, Charge>();
        CreateMap<UpdateChargeDto, Charge>();

        CreateMap<AppSettings, AppSettingsDto>();
        CreateMap<UpdateAppSettingsDto, AppSettings>();
    }
}
