namespace CoperativeTelouet.Business.DTOs.Client;

public record AvoirClientLigneDto(
    int Id,
    int AvoirClientId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateAvoirClientLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record AvoirClientDto(
    int Id,
    string Numero,
    int FactureClientId,
    int ClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Motif,
    bool RetourMarchandise,
    List<AvoirClientLigneDto> Lignes);

public record CreateAvoirClientDto(
    string Numero,
    int FactureClientId,
    int ClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Motif,
    bool RetourMarchandise,
    List<CreateAvoirClientLigneDto> Lignes);

public record UpdateAvoirClientDto(
    int FactureClientId,
    int ClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Motif,
    bool RetourMarchandise,
    List<CreateAvoirClientLigneDto> Lignes);

public record AvoirClientListItemDto(
    int Id,
    string Numero,
    int ClientId,
    string ClientNom,
    DateTime Date,
    decimal TotalTtc,
    string? Note);
