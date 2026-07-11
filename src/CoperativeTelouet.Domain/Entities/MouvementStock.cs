using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Domain.Entities;

public class MouvementStock : BaseEntity
{
    public int ProduitId { get; set; }
    public TypeMouvement Type { get; set; }
    public decimal StockAvant { get; set; }
    public decimal Quantite { get; set; }
    public string? OrigineType { get; set; }
    public int? OrigineId { get; set; }
    public string? Note { get; set; }

    public Produit Produit { get; set; } = null!;
}
