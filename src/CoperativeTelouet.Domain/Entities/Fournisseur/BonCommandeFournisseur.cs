using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class BonCommandeFournisseur : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int FournisseurId { get; set; }
    public int? DevisFournisseurId { get; set; }
    public int? FactureFournisseurId { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Fournisseur { get; set; } = null!;
    public DevisFournisseur? DevisFournisseur { get; set; }
    public FactureFournisseur? FactureFournisseur { get; set; }
    public ICollection<BonCommandeFournisseurLigne> Lignes { get; set; } = new List<BonCommandeFournisseurLigne>();
    public ICollection<BonReceptionFournisseur> BonsReception { get; set; } = new List<BonReceptionFournisseur>();
}
