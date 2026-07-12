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
    decimal TotalTtc,
    string? Motif,
    bool RetourMarchandise,
    List<AvoirFournisseurLigneDto> Lignes);

public record CreateAvoirFournisseurDto(
    string Numero,
    int FactureFournisseurId,
    int FournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Motif,
    bool RetourMarchandise,
    List<CreateAvoirFournisseurLigneDto> Lignes);

public record UpdateAvoirFournisseurDto(
    int FactureFournisseurId,
    int FournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Motif,
    bool RetourMarchandise,
    List<CreateAvoirFournisseurLigneDto> Lignes);

public record AvoirFournisseurListItemDto(
    int Id,
    string Numero,
    int FournisseurId,
    string FournisseurNom,
    int FactureFournisseurId,
    DateTime Date,
    decimal TotalTtc,
    string? Motif);
