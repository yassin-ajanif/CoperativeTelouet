using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class PaiementFournisseur : BaseEntity
{
    public int FactureFournisseurId { get; set; }
    public ModePaiement Mode { get; set; }
    public decimal Montant { get; set; }
    public DateTime Date { get; set; }
    public string? Reference { get; set; }

    public FactureFournisseur FactureFournisseur { get; set; } = null!;
}
