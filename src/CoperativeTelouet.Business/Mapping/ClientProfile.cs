using AutoMapper;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<DevisClientLigne, DevisClientLigneDto>();
        CreateMap<CreateDevisClientLigneDto, DevisClientLigne>();
        CreateMap<DevisClientCondition, DevisClientConditionDto>();
        CreateMap<CreateDevisClientConditionDto, DevisClientCondition>();
        CreateMap<DevisClient, DevisClientDto>();
        CreateMap<CreateDevisClientDto, DevisClient>();
        CreateMap<UpdateDevisClientDto, DevisClient>();

        CreateMap<BonCommandeClientLigne, BonCommandeClientLigneDto>();
        CreateMap<CreateBonCommandeClientLigneDto, BonCommandeClientLigne>();
        CreateMap<BonCommandeClient, BonCommandeClientDto>();
        CreateMap<CreateBonCommandeClientDto, BonCommandeClient>();
        CreateMap<UpdateBonCommandeClientDto, BonCommandeClient>();

        CreateMap<BonLivraisonClientLigne, BonLivraisonClientLigneDto>();
        CreateMap<CreateBonLivraisonClientLigneDto, BonLivraisonClientLigne>();
        CreateMap<BonLivraisonClient, BonLivraisonClientDto>();
        CreateMap<CreateBonLivraisonClientDto, BonLivraisonClient>();
        CreateMap<UpdateBonLivraisonClientDto, BonLivraisonClient>();

        CreateMap<FactureClientLigne, FactureClientLigneDto>();
        CreateMap<CreateFactureClientLigneDto, FactureClientLigne>();
        CreateMap<FactureClient, FactureClientDto>();
        CreateMap<CreateFactureClientDto, FactureClient>();
        CreateMap<UpdateFactureClientDto, FactureClient>();

        CreateMap<PaiementClient, PaiementClientDto>();
        CreateMap<CreatePaiementClientDto, PaiementClient>();
        CreateMap<UpdatePaiementClientDto, PaiementClient>();

        CreateMap<AvoirClientLigne, AvoirClientLigneDto>();
        CreateMap<CreateAvoirClientLigneDto, AvoirClientLigne>();
        CreateMap<AvoirClient, AvoirClientDto>();
        CreateMap<CreateAvoirClientDto, AvoirClient>();
        CreateMap<UpdateAvoirClientDto, AvoirClient>();
    }
}
