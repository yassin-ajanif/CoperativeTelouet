namespace CoperativeTelouet.Business.DTOs.Fournisseur;

public record BonCommandeFournisseurLigneDto(
    int Id,
    int BonCommandeFournisseurId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteCommandee,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateBonCommandeFournisseurLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteCommandee,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record BonCommandeFournisseurDto(
    int Id,
    string Numero,
    int FournisseurId,
    int? DevisFournisseurId,
    int? FactureFournisseurId,
    DateTime Date,
    string? Note,
    List<BonCommandeFournisseurLigneDto> Lignes);

public record CreateBonCommandeFournisseurDto(
    string Numero,
    int FournisseurId,
    int? DevisFournisseurId,
    DateTime Date,
    string? Note,
    List<CreateBonCommandeFournisseurLigneDto> Lignes);

public record UpdateBonCommandeFournisseurDto(DateTime Date, string? Note);
