using AutoMapper;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Business.Mapping;

public class FournisseurProfile : Profile
{
    public FournisseurProfile()
    {
        CreateMap<DevisFournisseurLigne, DevisFournisseurLigneDto>();
        CreateMap<CreateDevisFournisseurLigneDto, DevisFournisseurLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.DevisFournisseurId, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<DevisFournisseurCondition, DevisFournisseurConditionDto>();
        CreateMap<CreateDevisFournisseurConditionDto, DevisFournisseurCondition>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.DevisFournisseurId, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore());
        CreateMap<DevisFournisseur, DevisFournisseurDto>()
            .ForMember(d => d.Lignes, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore());
        CreateMap<CreateDevisFournisseurDto, DevisFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsReception, o => o.Ignore())
            .ForMember(d => d.Factures, o => o.Ignore());
        CreateMap<UpdateDevisFournisseurDto, DevisFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsReception, o => o.Ignore())
            .ForMember(d => d.Factures, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.DevisFournisseurId = entity.Id;
                }
            });

        CreateMap<BonCommandeFournisseurLigne, BonCommandeFournisseurLigneDto>();
        CreateMap<CreateBonCommandeFournisseurLigneDto, BonCommandeFournisseurLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.BonCommandeFournisseurId, o => o.Ignore())
            .ForMember(d => d.BonCommandeFournisseur, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<BonCommandeFournisseur, BonCommandeFournisseurDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateBonCommandeFournisseurDto, BonCommandeFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureFournisseurId, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.BonsReception, o => o.Ignore());
        CreateMap<UpdateBonCommandeFournisseurDto, BonCommandeFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.FactureFournisseurId, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.BonsReception, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.BonCommandeFournisseurId = entity.Id;
                }
            });

        CreateMap<BonReceptionFournisseurLigne, BonReceptionFournisseurLigneDto>();
        CreateMap<CreateBonReceptionFournisseurLigneDto, BonReceptionFournisseurLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.BonReceptionFournisseurId, o => o.Ignore())
            .ForMember(d => d.BonReceptionFournisseur, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<BonReceptionFournisseur, BonReceptionFournisseurDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateBonReceptionFournisseurDto, BonReceptionFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureFournisseurId, o => o.Ignore())
            .ForMember(d => d.BonCommandeFournisseur, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.FactureLignes, o => o.Ignore());
        CreateMap<UpdateBonReceptionFournisseurDto, BonReceptionFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.FactureFournisseurId, o => o.Ignore())
            .ForMember(d => d.BonCommandeFournisseur, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.FactureLignes, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.BonReceptionFournisseurId = entity.Id;
                }
            });

        CreateMap<FactureFournisseurLigne, FactureFournisseurLigneDto>();
        CreateMap<CreateFactureFournisseurLigneDto, FactureFournisseurLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureFournisseurId, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.BonReceptionFournisseur, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<FactureFournisseur, FactureFournisseurDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateFactureFournisseurDto, FactureFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.EstPayee, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.Paiements, o => o.Ignore())
            .ForMember(d => d.Avoirs, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsReception, o => o.Ignore());
        CreateMap<UpdateFactureFournisseurDto, FactureFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .ForMember(d => d.DevisFournisseur, o => o.Ignore())
            .ForMember(d => d.Paiements, o => o.Ignore())
            .ForMember(d => d.Avoirs, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsReception, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.FactureFournisseurId = entity.Id;
                }
            });

        CreateMap<PaiementFournisseur, PaiementFournisseurDto>();
        CreateMap<CreatePaiementFournisseurDto, PaiementFournisseur>();
        CreateMap<UpdatePaiementFournisseurDto, PaiementFournisseur>();

        CreateMap<AvoirFournisseurLigne, AvoirFournisseurLigneDto>();
        CreateMap<CreateAvoirFournisseurLigneDto, AvoirFournisseurLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.AvoirFournisseurId, o => o.Ignore())
            .ForMember(d => d.AvoirFournisseur, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<AvoirFournisseur, AvoirFournisseurDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateAvoirFournisseurDto, AvoirFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore());
        CreateMap<UpdateAvoirFournisseurDto, AvoirFournisseur>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.FactureFournisseur, o => o.Ignore())
            .ForMember(d => d.Fournisseur, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.AvoirFournisseurId = entity.Id;
                }
            });
    }
}
