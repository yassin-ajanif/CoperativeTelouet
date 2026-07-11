using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Domain.Entities.Client;

public class PaiementClient : BaseEntity
{
    public int FactureClientId { get; set; }
    public ModePaiement Mode { get; set; }
    public decimal Montant { get; set; }
    public DateTime Date { get; set; }
    public string? Reference { get; set; }

    public FactureClient FactureClient { get; set; } = null!;
}
