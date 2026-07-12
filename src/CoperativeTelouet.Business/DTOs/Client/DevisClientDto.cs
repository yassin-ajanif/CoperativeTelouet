namespace CoperativeTelouet.Business.DTOs.Client;

public record DevisClientLigneDto(
    int Id,
    int DevisClientId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateDevisClientLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record DevisClientConditionDto(int Id, int DevisClientId, string Titre, string Valeur, int Ordre);

public record CreateDevisClientConditionDto(string Titre, string Valeur, int Ordre);

public record DevisClientDto(
    int Id,
    string Numero,
    int ClientId,
    DateTime Date,
    DateTime DateValidite,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<DevisClientLigneDto> Lignes,
    List<DevisClientConditionDto> Conditions);

public record CreateDevisClientDto(
    string Numero,
    int ClientId,
    DateTime Date,
    DateTime DateValidite,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<CreateDevisClientLigneDto> Lignes,
    List<CreateDevisClientConditionDto>? Conditions = null);

public record UpdateDevisClientDto(
    int ClientId,
    DateTime Date,
    DateTime DateValidite,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<CreateDevisClientLigneDto> Lignes);

public record DevisClientListItemDto(
    int Id,
    string Numero,
    int ClientId,
    string ClientNom,
    DateTime Date,
    DateTime DateValidite,
    decimal TotalTtc,
    string? Note);

public record ArticleSuggestionDto(
    int? ProduitId,
    int? ServiceId,
    string Reference,
    string Designation,
    string? Unite,
    decimal PrixUnitaireHT,
    decimal TauxTVA);
