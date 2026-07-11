using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class AvoirFournisseurLigne : BaseEntity
{
    public int AvoirFournisseurId { get; set; }
    public int? ProduitId { get; set; }
    public int? ServiceId { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal Quantite { get; set; }
    public decimal PrixUnitaireHT { get; set; }
    public decimal Remise { get; set; }
    public decimal TauxTVA { get; set; }
    public string? Conditionnement { get; set; }

    public AvoirFournisseur AvoirFournisseur { get; set; } = null!;
    public Entities.Produit? Produit { get; set; }
    public Entities.ServiceItem? Service { get; set; }
}
