namespace CoperativeTelouet.Business.DTOs.Client;

public record BonCommandeClientLigneDto(
    int Id,
    int BonCommandeClientId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteCommandee,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateBonCommandeClientLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteCommandee,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record BonCommandeClientDto(
    int Id,
    string Numero,
    int ClientId,
    int? DevisClientId,
    int? FactureClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<BonCommandeClientLigneDto> Lignes);

public record CreateBonCommandeClientDto(
    string Numero,
    int ClientId,
    int? DevisClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<CreateBonCommandeClientLigneDto> Lignes);

public record UpdateBonCommandeClientDto(
    int ClientId,
    int? DevisClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<CreateBonCommandeClientLigneDto> Lignes);

public record BonCommandeClientListItemDto(
    int Id,
    string Numero,
    int ClientId,
    string ClientNom,
    DateTime Date,
    decimal TotalTtc,
    string? Note);
