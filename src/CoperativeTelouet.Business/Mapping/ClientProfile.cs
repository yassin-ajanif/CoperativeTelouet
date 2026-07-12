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
        CreateMap<CreateDevisClientConditionDto, DevisClientCondition>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.DevisClientId, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore());
        CreateMap<DevisClient, DevisClientDto>()
            .ForMember(d => d.Lignes, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore());
        CreateMap<CreateDevisClientDto, DevisClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .ForMember(d => d.Factures, o => o.Ignore());
        CreateMap<UpdateDevisClientDto, DevisClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.Conditions, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .ForMember(d => d.Factures, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.DevisClientId = entity.Id;
                }
            });

        CreateMap<BonCommandeClientLigne, BonCommandeClientLigneDto>();
        CreateMap<CreateBonCommandeClientLigneDto, BonCommandeClientLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.BonCommandeClientId, o => o.Ignore())
            .ForMember(d => d.BonCommandeClient, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<BonCommandeClient, BonCommandeClientDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateBonCommandeClientDto, BonCommandeClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureClientId, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore());
        CreateMap<UpdateBonCommandeClientDto, BonCommandeClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.FactureClientId, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.BonCommandeClientId = entity.Id;
                }
            });

        CreateMap<BonLivraisonClientLigne, BonLivraisonClientLigneDto>();
        CreateMap<CreateBonLivraisonClientLigneDto, BonLivraisonClientLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.BonLivraisonClientId, o => o.Ignore())
            .ForMember(d => d.BonLivraisonClient, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<BonLivraisonClient, BonLivraisonClientDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateBonLivraisonClientDto, BonLivraisonClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureClientId, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.BonCommandeClient, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore())
            .ForMember(d => d.FactureLignes, o => o.Ignore());
        CreateMap<UpdateBonLivraisonClientDto, BonLivraisonClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.FactureClientId, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.BonCommandeClient, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore())
            .ForMember(d => d.FactureLignes, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.BonLivraisonClientId = entity.Id;
                }
            });

        CreateMap<FactureClientLigne, FactureClientLigneDto>();
        CreateMap<CreateFactureClientLigneDto, FactureClientLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.FactureClientId, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore())
            .ForMember(d => d.BonLivraisonClient, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<FactureClient, FactureClientDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateFactureClientDto, FactureClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.EstPayee, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.Paiements, o => o.Ignore())
            .ForMember(d => d.Avoirs, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .ForMember(d => d.BonsSortieStockage, o => o.Ignore());
        CreateMap<UpdateFactureClientDto, FactureClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.DevisClient, o => o.Ignore())
            .ForMember(d => d.Paiements, o => o.Ignore())
            .ForMember(d => d.Avoirs, o => o.Ignore())
            .ForMember(d => d.BonsCommande, o => o.Ignore())
            .ForMember(d => d.BonsLivraison, o => o.Ignore())
            .ForMember(d => d.BonsSortieStockage, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.FactureClientId = entity.Id;
                }
            });

        CreateMap<PaiementClient, PaiementClientDto>();
        CreateMap<CreatePaiementClientDto, PaiementClient>();
        CreateMap<UpdatePaiementClientDto, PaiementClient>();

        CreateMap<AvoirClientLigne, AvoirClientLigneDto>();
        CreateMap<CreateAvoirClientLigneDto, AvoirClientLigne>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.AvoirClientId, o => o.Ignore())
            .ForMember(d => d.AvoirClient, o => o.Ignore())
            .ForMember(d => d.Produit, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
        CreateMap<AvoirClient, AvoirClientDto>()
            .ForMember(d => d.Lignes, o => o.Ignore());
        CreateMap<CreateAvoirClientDto, AvoirClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore());
        CreateMap<UpdateAvoirClientDto, AvoirClient>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Numero, o => o.Ignore())
            .ForMember(d => d.Client, o => o.Ignore())
            .ForMember(d => d.FactureClient, o => o.Ignore())
            .AfterMap((_, entity) =>
            {
                foreach (var line in entity.Lignes)
                {
                    line.Id = 0;
                    line.AvoirClientId = entity.Id;
                }
            });
    }
}
