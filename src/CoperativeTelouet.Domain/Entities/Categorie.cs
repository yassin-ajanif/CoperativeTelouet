using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities;

public class Categorie : BaseEntity
{
    public string Nom { get; set; } = string.Empty;

    public ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
