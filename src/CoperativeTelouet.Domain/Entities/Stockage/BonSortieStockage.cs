using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Entities.Client;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Domain.Entities.Stockage;

public class BonSortieStockage : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public DateTime DateSortie { get; set; }
    public EtatBac EtatBac { get; set; }
    public int NombreBacs { get; set; }
    public int? BonEntreeStockageId { get; set; }
    public int? FactureClientId { get; set; }
    public int BacsVidesAvant { get; set; }
    public int BacsVidesApres { get; set; }
    public int BacsPleinsAvant { get; set; }
    public int BacsPleinsApres { get; set; }
    public string? Note { get; set; }

    public Entities.Tiers Client { get; set; } = null!;
    public BonEntreeStockage? BonEntreeStockage { get; set; }
    public FactureClient? FactureClient { get; set; }
}
