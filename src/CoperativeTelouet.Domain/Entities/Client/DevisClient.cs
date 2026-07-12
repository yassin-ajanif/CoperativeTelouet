using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class DevisClient : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DateValidite { get; set; }
    public decimal RemiseGlobale { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Client { get; set; } = null!;
    public ICollection<DevisClientLigne> Lignes { get; set; } = new List<DevisClientLigne>();
    public ICollection<DevisClientCondition> Conditions { get; set; } = new List<DevisClientCondition>();
    public ICollection<BonCommandeClient> BonsCommande { get; set; } = new List<BonCommandeClient>();
    public ICollection<BonLivraisonClient> BonsLivraison { get; set; } = new List<BonLivraisonClient>();
    public ICollection<FactureClient> Factures { get; set; } = new List<FactureClient>();
}
