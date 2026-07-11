using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Client;

public class DevisClientCondition : BaseEntity
{
    public int DevisClientId { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Valeur { get; set; } = string.Empty;
    public int Ordre { get; set; }

    public DevisClient DevisClient { get; set; } = null!;
}
