using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Stockage;

public class StockBacsSociete : BaseEntity
{
    public int BacsVides { get; set; }
    public int BacsPleins { get; set; }
    public int TotalBacsOriginal { get; set; }
}
