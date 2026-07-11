namespace CoperativeTelouet.Business.DTOs.Fournisseur;

public record AvoirFournisseurLigneDto(
    int Id,
    int AvoirFournisseurId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateAvoirFournisseurLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record AvoirFournisseurDto(
    int Id,
    string Numero,
    int FactureFournisseurId,
    int FournisseurId,
    DateTime Date,
    string? Motif,
    bool RetourMarchandise,
    List<AvoirFournisseurLigneDto> Lignes);

public record CreateAvoirFournisseurDto(
    string Numero,
    int FactureFournisseurId,
    int FournisseurId,
    DateTime Date,
    string? Motif,
    bool RetourMarchandise,
    List<CreateAvoirFournisseurLigneDto> Lignes);

public record UpdateAvoirFournisseurDto(
    DateTime Date,
    string? Motif,
    bool RetourMarchandise);
