using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class AvoirClient : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int FactureClientId { get; set; }
    public int ClientId { get; set; }
    public DateTime Date { get; set; }
    public string? Motif { get; set; }
    public bool RetourMarchandise { get; set; }

    public FactureClient FactureClient { get; set; } = null!;
    public Entities.Tiers Client { get; set; } = null!;
    public ICollection<AvoirClientLigne> Lignes { get; set; } = new List<AvoirClientLigne>();
}
