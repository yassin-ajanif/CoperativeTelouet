using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Entities.Stockage;

namespace CoperativeTelouet.Domain.Entities.Client;

public class FactureClient : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int? DevisClientId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? DateEcheance { get; set; }
    public bool EstPayee { get; set; }
    public decimal RemiseGlobale { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }
    public string? BonCommandeReference { get; set; }

    public Entities.Tiers Client { get; set; } = null!;
    public DevisClient? DevisClient { get; set; }
    public ICollection<FactureClientLigne> Lignes { get; set; } = new List<FactureClientLigne>();
    public ICollection<PaiementClient> Paiements { get; set; } = new List<PaiementClient>();
    public ICollection<AvoirClient> Avoirs { get; set; } = new List<AvoirClient>();
    public ICollection<BonCommandeClient> BonsCommande { get; set; } = new List<BonCommandeClient>();
    public ICollection<BonLivraisonClient> BonsLivraison { get; set; } = new List<BonLivraisonClient>();
    public ICollection<BonSortieStockage> BonsSortieStockage { get; set; } = new List<BonSortieStockage>();
}
