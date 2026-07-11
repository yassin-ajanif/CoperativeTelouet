using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Domain.Entities.Stockage;

public class BonEntreeStockage : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public DateTime DateEntree { get; set; }
    public EtatBac EtatBac { get; set; }
    public int? ChambreFroideId { get; set; }
    public int? VarieteId { get; set; }
    public string? NumeroLot { get; set; }
    public int NombreBacs { get; set; }
    public decimal? PrixParBacParJourApplique { get; set; }
    public int BacsVidesAvant { get; set; }
    public int BacsVidesApres { get; set; }
    public int BacsPleinsAvant { get; set; }
    public int BacsPleinsApres { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Client { get; set; } = null!;
    public ChambreFroide? ChambreFroide { get; set; }
    public VarietePomme? Variete { get; set; }
    public ICollection<BonSortieStockage> Sorties { get; set; } = new List<BonSortieStockage>();
}
