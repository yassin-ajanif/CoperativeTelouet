using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Stockage;

public class VarietePomme : BaseEntity
{
    public string Nom { get; set; } = string.Empty;

    public ICollection<BonEntreeStockage> BonsEntree { get; set; } = new List<BonEntreeStockage>();
}
