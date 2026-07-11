using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class DevisClientLigne : BaseEntity
{
    public int DevisClientId { get; set; }
    public int? ProduitId { get; set; }
    public int? ServiceId { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal Quantite { get; set; }
    public decimal PrixUnitaireHT { get; set; }
    public decimal Remise { get; set; }
    public decimal TauxTVA { get; set; }
    public string? Conditionnement { get; set; }

    public DevisClient DevisClient { get; set; } = null!;
    public Entities.Produit? Produit { get; set; }
    public Entities.ServiceItem? Service { get; set; }
}
