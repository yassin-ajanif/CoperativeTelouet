using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Stockage;

public class ChambreFroide : BaseEntity
{
    public string Nom { get; set; } = string.Empty;
    public int CapaciteBacs { get; set; }
    public bool Actif { get; set; }

    public ICollection<BonEntreeStockage> BonsEntree { get; set; } = new List<BonEntreeStockage>();
}
