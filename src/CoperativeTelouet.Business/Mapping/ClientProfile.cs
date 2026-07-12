using AutoMapper;
using CoperativeTelouet.Business.DTOs.Client;
using CoperativeTelouet.Domain.Entities.Client;

namespace CoperativeTelouet.Business.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<DevisClientLigne, DevisClientLigneDto>();
        CreateMap<CreateDevisClientLigneDto, DevisClientLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.DevisClientId, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<DevisClientCondition, DevisClientConditionDto>();
        CreateMap<CreateDevisClientConditionDto, DevisClientCondition>();
        CreateMap<DevisClient, DevisClientDto>()
            .ForMember(d => d.Lignes, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore());
        CreateMap<CreateDevisClientDto, DevisClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Lignes, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .ForMember(d => d.Factures, o => o.Ignore());
        CreateMap<UpdateDevisClientDto, DevisClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.Lignes, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .ForMember(d => d.Factures, o => o.Ignore());

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
