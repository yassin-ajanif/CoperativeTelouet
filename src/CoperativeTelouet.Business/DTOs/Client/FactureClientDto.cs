namespace CoperativeTelouet.Business.DTOs.Client;

public record FactureClientLigneDto(
    int Id,
    int FactureClientId,
    int? BonLivraisonClientId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateFactureClientLigneDto(
    int? BonLivraisonClientId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record FactureClientDto(
    int Id,
    string Numero,
    int ClientId,
    int? DevisClientId,
    DateTime Date,
    DateTime? DateEcheance,
    bool EstPayee,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    string? BonCommandeReference,
    List<FactureClientLigneDto> Lignes);

public record CreateFactureClientDto(
    string Numero,
    int ClientId,
    int? DevisClientId,
    DateTime Date,
    DateTime? DateEcheance,
    decimal RemiseGlobale,
    string? Note,
    string? BonCommandeReference,
    List<CreateFactureClientLigneDto> Lignes);

public record UpdateFactureClientDto(
    DateTime Date,
    DateTime? DateEcheance,
    bool EstPayee,
    decimal RemiseGlobale,
    string? Note,
    string? BonCommandeReference);
