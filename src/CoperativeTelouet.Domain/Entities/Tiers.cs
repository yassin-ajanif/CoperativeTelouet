using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Entities.Client;
using CoperativeTelouet.Domain.Entities.Fournisseur;
using CoperativeTelouet.Domain.Entities.Stockage;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Domain.Entities;

public class Tiers : BaseEntity
{
    public TypeTiers Type { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? ICE { get; set; }
    public string? Adresse { get; set; }
    public string? Ville { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? ConditionsPaiement { get; set; }
    public bool Actif { get; set; }

    public ICollection<Charge> Charges { get; set; } = new List<Charge>();
    public ICollection<DevisClient> DevisClients { get; set; } = new List<DevisClient>();
    public ICollection<BonCommandeClient> BonsCommandeClient { get; set; } = new List<BonCommandeClient>();
    public ICollection<BonLivraisonClient> BonsLivraisonClient { get; set; } = new List<BonLivraisonClient>();
    public ICollection<FactureClient> FacturesClient { get; set; } = new List<FactureClient>();
    public ICollection<AvoirClient> AvoirsClient { get; set; } = new List<AvoirClient>();
    public ICollection<DevisFournisseur> DevisFournisseur { get; set; } = new List<DevisFournisseur>();
    public ICollection<BonCommandeFournisseur> BonsCommandeFournisseur { get; set; } = new List<BonCommandeFournisseur>();
    public ICollection<BonReceptionFournisseur> BonsReceptionFournisseur { get; set; } = new List<BonReceptionFournisseur>();
    public ICollection<FactureFournisseur> FacturesFournisseur { get; set; } = new List<FactureFournisseur>();
    public ICollection<AvoirFournisseur> AvoirsFournisseur { get; set; } = new List<AvoirFournisseur>();
    public ICollection<BonEntreeStockage> BonsEntreeStockage { get; set; } = new List<BonEntreeStockage>();
    public ICollection<BonSortieStockage> BonsSortieStockage { get; set; } = new List<BonSortieStockage>();
}
