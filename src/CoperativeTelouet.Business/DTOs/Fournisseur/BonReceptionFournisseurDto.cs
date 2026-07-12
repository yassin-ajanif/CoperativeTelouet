namespace CoperativeTelouet.Business.DTOs.Fournisseur;

public record BonReceptionFournisseurLigneDto(
    int Id,
    int BonReceptionFournisseurId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteRecue,
    decimal PrixUnitaireHT,
    decimal TauxTVA);

public record CreateBonReceptionFournisseurLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteRecue,
    decimal PrixUnitaireHT,
    decimal TauxTVA);

public record BonReceptionFournisseurDto(
    int Id,
    string Numero,
    int BonCommandeFournisseurId,
    int FournisseurId,
    int? DevisFournisseurId,
    int? FactureFournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<BonReceptionFournisseurLigneDto> Lignes);

public record CreateBonReceptionFournisseurDto(
    string Numero,
    int BonCommandeFournisseurId,
    int FournisseurId,
    int? DevisFournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<CreateBonReceptionFournisseurLigneDto> Lignes);

public record UpdateBonReceptionFournisseurDto(
    int BonCommandeFournisseurId,
    int FournisseurId,
    int? DevisFournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<CreateBonReceptionFournisseurLigneDto> Lignes);

public record BonReceptionFournisseurListItemDto(
    int Id,
    string Numero,
    int FournisseurId,
    string FournisseurNom,
    int BonCommandeFournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Note);
