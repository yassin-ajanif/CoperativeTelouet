using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities.Fournisseur;

public class DevisFournisseurCondition : BaseEntity
{
    public int DevisFournisseurId { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Valeur { get; set; } = string.Empty;
    public int Ordre { get; set; }

    public DevisFournisseur DevisFournisseur { get; set; } = null!;
}
