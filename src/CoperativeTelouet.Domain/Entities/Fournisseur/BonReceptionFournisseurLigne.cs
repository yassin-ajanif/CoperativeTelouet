using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class BonReceptionFournisseurLigne : BaseEntity
{
    public int BonReceptionFournisseurId { get; set; }
    public int? ProduitId { get; set; }
    public int? ServiceId { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal QuantiteRecue { get; set; }
    public decimal PrixUnitaireHT { get; set; }
    public decimal TauxTVA { get; set; }

    public BonReceptionFournisseur BonReceptionFournisseur { get; set; } = null!;
    public Entities.Produit? Produit { get; set; }
    public Entities.ServiceItem? Service { get; set; }
}
