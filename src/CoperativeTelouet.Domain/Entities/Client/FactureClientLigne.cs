using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class FactureClientLigne : BaseEntity
{
    public int FactureClientId { get; set; }
    public int? BonLivraisonClientId { get; set; }
    public int? ProduitId { get; set; }
    public int? ServiceId { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal Quantite { get; set; }
    public decimal PrixUnitaireHT { get; set; }
    public decimal Remise { get; set; }
    public decimal TauxTVA { get; set; }
    public string? Conditionnement { get; set; }

    public FactureClient FactureClient { get; set; } = null!;
    public BonLivraisonClient? BonLivraisonClient { get; set; }
    public Entities.Produit? Produit { get; set; }
    public Entities.ServiceItem? Service { get; set; }
}
