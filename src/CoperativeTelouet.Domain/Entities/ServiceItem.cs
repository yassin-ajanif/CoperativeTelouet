using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Entities.Client;
using CoperativeTelouet.Domain.Entities.Fournisseur;

namespace CoperativeTelouet.Domain.Entities;

public class ServiceItem : BaseEntity
{
    public string Reference { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string? Unite { get; set; }
    public decimal PrixVenteHT { get; set; }
    public decimal CoutHT { get; set; }
    public decimal TauxTVA { get; set; }
    public bool Actif { get; set; }
    public string? Note { get; set; }
    public byte[]? ImageData { get; set; }

    public ICollection<DevisClientLigne> DevisClientLignes { get; set; } = new List<DevisClientLigne>();
    public ICollection<BonCommandeClientLigne> BonCommandeClientLignes { get; set; } = new List<BonCommandeClientLigne>();
    public ICollection<BonLivraisonClientLigne> BonLivraisonClientLignes { get; set; } = new List<BonLivraisonClientLigne>();
    public ICollection<FactureClientLigne> FactureClientLignes { get; set; } = new List<FactureClientLigne>();
    public ICollection<AvoirClientLigne> AvoirClientLignes { get; set; } = new List<AvoirClientLigne>();
    public ICollection<DevisFournisseurLigne> DevisFournisseurLignes { get; set; } = new List<DevisFournisseurLigne>();
    public ICollection<BonCommandeFournisseurLigne> BonCommandeFournisseurLignes { get; set; } = new List<BonCommandeFournisseurLigne>();
    public ICollection<BonReceptionFournisseurLigne> BonReceptionFournisseurLignes { get; set; } = new List<BonReceptionFournisseurLigne>();
    public ICollection<FactureFournisseurLigne> FactureFournisseurLignes { get; set; } = new List<FactureFournisseurLigne>();
    public ICollection<AvoirFournisseurLigne> AvoirFournisseurLignes { get; set; } = new List<AvoirFournisseurLigne>();
}
