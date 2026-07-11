using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class AvoirFournisseur : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int FactureFournisseurId { get; set; }
    public int FournisseurId { get; set; }
    public DateTime Date { get; set; }
    public string? Motif { get; set; }
    public bool RetourMarchandise { get; set; }

    public FactureFournisseur FactureFournisseur { get; set; } = null!;
    public Entities.Tiers Fournisseur { get; set; } = null!;
    public ICollection<AvoirFournisseurLigne> Lignes { get; set; } = new List<AvoirFournisseurLigne>();
}
