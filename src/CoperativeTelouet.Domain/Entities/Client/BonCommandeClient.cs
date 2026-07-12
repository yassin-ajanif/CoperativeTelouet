using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class BonCommandeClient : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int? DevisClientId { get; set; }
    public int? FactureClientId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Client { get; set; } = null!;
    public DevisClient? DevisClient { get; set; }
    public FactureClient? FactureClient { get; set; }
    public ICollection<BonCommandeClientLigne> Lignes { get; set; } = new List<BonCommandeClientLigne>();
    public ICollection<BonLivraisonClient> BonsLivraison { get; set; } = new List<BonLivraisonClient>();
}
