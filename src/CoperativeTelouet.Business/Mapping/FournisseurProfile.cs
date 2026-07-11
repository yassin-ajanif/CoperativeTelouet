using AutoMapper;
using CoperativeTelouet.Business.DTOs.Fournisseur;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Business.Mapping;

public class FournisseurProfile : Profile
{
    public FournisseurProfile()
    {
        CreateMap<DevisFournisseurLigne, DevisFournisseurLigneDto>();
        CreateMap<CreateDevisFournisseurLigneDto, DevisFournisseurLigne>();
        CreateMap<DevisFournisseurCondition, DevisFournisseurConditionDto>();
        CreateMap<CreateDevisFournisseurConditionDto, DevisFournisseurCondition>();
        CreateMap<DevisFournisseur, DevisFournisseurDto>();
        CreateMap<CreateDevisFournisseurDto, DevisFournisseur>();
        CreateMap<UpdateDevisFournisseurDto, DevisFournisseur>();

        CreateMap<BonCommandeFournisseurLigne, BonCommandeFournisseurLigneDto>();
        CreateMap<CreateBonCommandeFournisseurLigneDto, BonCommandeFournisseurLigne>();
        CreateMap<BonCommandeFournisseur, BonCommandeFournisseurDto>();
        CreateMap<CreateBonCommandeFournisseurDto, BonCommandeFournisseur>();
        CreateMap<UpdateBonCommandeFournisseurDto, BonCommandeFournisseur>();

        CreateMap<BonReceptionFournisseurLigne, BonReceptionFournisseurLigneDto>();
        CreateMap<CreateBonReceptionFournisseurLigneDto, BonReceptionFournisseurLigne>();
        CreateMap<BonReceptionFournisseur, BonReceptionFournisseurDto>();
        CreateMap<CreateBonReceptionFournisseurDto, BonReceptionFournisseur>();
        CreateMap<UpdateBonReceptionFournisseurDto, BonReceptionFournisseur>();

        CreateMap<FactureFournisseurLigne, FactureFournisseurLigneDto>();
        CreateMap<CreateFactureFournisseurLigneDto, FactureFournisseurLigne>();
        CreateMap<FactureFournisseur, FactureFournisseurDto>();
        CreateMap<CreateFactureFournisseurDto, FactureFournisseur>();
        CreateMap<UpdateFactureFournisseurDto, FactureFournisseur>();

        CreateMap<PaiementFournisseur, PaiementFournisseurDto>();
        CreateMap<CreatePaiementFournisseurDto, PaiementFournisseur>();
        CreateMap<UpdatePaiementFournisseurDto, PaiementFournisseur>();

        CreateMap<AvoirFournisseurLigne, AvoirFournisseurLigneDto>();
        CreateMap<CreateAvoirFournisseurLigneDto, AvoirFournisseurLigne>();
        CreateMap<AvoirFournisseur, AvoirFournisseurDto>();
        CreateMap<CreateAvoirFournisseurDto, AvoirFournisseur>();
        CreateMap<UpdateAvoirFournisseurDto, AvoirFournisseur>();
    }
}
