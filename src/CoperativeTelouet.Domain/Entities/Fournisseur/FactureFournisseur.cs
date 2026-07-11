using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class FactureFournisseur : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int FournisseurId { get; set; }
    public int? DevisFournisseurId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? DateEcheance { get; set; }
    public bool EstPayee { get; set; }
    public decimal RemiseGlobale { get; set; }
    public decimal TotalTtc { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Fournisseur { get; set; } = null!;
    public DevisFournisseur? DevisFournisseur { get; set; }
    public ICollection<FactureFournisseurLigne> Lignes { get; set; } = new List<FactureFournisseurLigne>();
    public ICollection<PaiementFournisseur> Paiements { get; set; } = new List<PaiementFournisseur>();
    public ICollection<AvoirFournisseur> Avoirs { get; set; } = new List<AvoirFournisseur>();
    public ICollection<BonCommandeFournisseur> BonsCommande { get; set; } = new List<BonCommandeFournisseur>();
    public ICollection<BonReceptionFournisseur> BonsReception { get; set; } = new List<BonReceptionFournisseur>();
}
