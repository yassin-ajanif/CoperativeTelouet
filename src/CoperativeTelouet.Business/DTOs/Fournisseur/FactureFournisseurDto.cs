namespace CoperativeTelouet.Business.DTOs.Fournisseur;

public record FactureFournisseurLigneDto(
    int Id,
    int FactureFournisseurId,
    int? BonReceptionFournisseurId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateFactureFournisseurLigneDto(
    int? BonReceptionFournisseurId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record FactureFournisseurDto(
    int Id,
    string Numero,
    int FournisseurId,
    int? DevisFournisseurId,
    DateTime Date,
    DateTime? DateEcheance,
    bool EstPayee,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<FactureFournisseurLigneDto> Lignes);

public record CreateFactureFournisseurDto(
    string Numero,
    int FournisseurId,
    int? DevisFournisseurId,
    DateTime Date,
    DateTime? DateEcheance,
    decimal RemiseGlobale,
    string? Note,
    List<CreateFactureFournisseurLigneDto> Lignes);

public record UpdateFactureFournisseurDto(
    DateTime Date,
    DateTime? DateEcheance,
    bool EstPayee,
    decimal RemiseGlobale,
    string? Note);
