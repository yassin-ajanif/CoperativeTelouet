using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities;

public class Charge : BaseEntity
{
    public int TypeChargeId { get; set; }
    public DateTime Date { get; set; }
    public string Libelle { get; set; } = string.Empty;
    public int? FournisseurId { get; set; }
    public string? BeneficiaireLibre { get; set; }
    public decimal MontantTtc { get; set; }
    public string? Note { get; set; }

    public TypeCharge TypeCharge { get; set; } = null!;
    public Tiers? Fournisseur { get; set; }
}
