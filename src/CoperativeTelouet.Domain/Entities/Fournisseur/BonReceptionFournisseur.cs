using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class BonReceptionFournisseur : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int BonCommandeFournisseurId { get; set; }
    public int FournisseurId { get; set; }
    public int? DevisFournisseurId { get; set; }
    public int? FactureFournisseurId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }

    public BonCommandeFournisseur BonCommandeFournisseur { get; set; } = null!;
    public Entities.Tiers Fournisseur { get; set; } = null!;
    public DevisFournisseur? DevisFournisseur { get; set; }
    public FactureFournisseur? FactureFournisseur { get; set; }
    public ICollection<BonReceptionFournisseurLigne> Lignes { get; set; } = new List<BonReceptionFournisseurLigne>();
    public ICollection<FactureFournisseurLigne> FactureLignes { get; set; } = new List<FactureFournisseurLigne>();
}
