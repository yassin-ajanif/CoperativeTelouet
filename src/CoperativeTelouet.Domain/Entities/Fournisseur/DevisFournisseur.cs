using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class DevisFournisseur : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int FournisseurId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DateValidite { get; set; }
    public decimal RemiseGlobale { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Fournisseur { get; set; } = null!;
    public ICollection<DevisFournisseurLigne> Lignes { get; set; } = new List<DevisFournisseurLigne>();
    public ICollection<DevisFournisseurCondition> Conditions { get; set; } = new List<DevisFournisseurCondition>();
    public ICollection<BonCommandeFournisseur> BonsCommande { get; set; } = new List<BonCommandeFournisseur>();
    public ICollection<BonReceptionFournisseur> BonsReception { get; set; } = new List<BonReceptionFournisseur>();
    public ICollection<FactureFournisseur> Factures { get; set; } = new List<FactureFournisseur>();
}
