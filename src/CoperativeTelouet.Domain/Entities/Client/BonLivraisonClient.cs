using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class BonLivraisonClient : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int? DevisClientId { get; set; }
    public int? BonCommandeClientId { get; set; }
    public int? FactureClientId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Client { get; set; } = null!;
    public DevisClient? DevisClient { get; set; }
    public BonCommandeClient? BonCommandeClient { get; set; }
    public FactureClient? FactureClient { get; set; }
    public ICollection<BonLivraisonClientLigne> Lignes { get; set; } = new List<BonLivraisonClientLigne>();
    public ICollection<FactureClientLigne> FactureLignes { get; set; } = new List<FactureClientLigne>();
}
