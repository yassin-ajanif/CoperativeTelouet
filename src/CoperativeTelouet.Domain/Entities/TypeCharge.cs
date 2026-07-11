using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities;

public class TypeCharge : BaseEntity
{
    public string Nom { get; set; } = string.Empty;
    public bool Actif { get; set; }

    public ICollection<Charge> Charges { get; set; } = new List<Charge>();
}
