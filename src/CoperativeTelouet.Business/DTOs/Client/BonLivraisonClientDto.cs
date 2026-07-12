namespace CoperativeTelouet.Business.DTOs.Client;

public record BonLivraisonClientLigneDto(
    int Id,
    int BonLivraisonClientId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteCommandee,
    decimal QuantiteLivree,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA);

public record CreateBonLivraisonClientLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal QuantiteCommandee,
    decimal QuantiteLivree,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA);

public record BonLivraisonClientDto(
    int Id,
    string Numero,
    int ClientId,
    int? DevisClientId,
    int? BonCommandeClientId,
    int? FactureClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<BonLivraisonClientLigneDto> Lignes);

public record CreateBonLivraisonClientDto(
    string Numero,
    int ClientId,
    int? DevisClientId,
    int? BonCommandeClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<CreateBonLivraisonClientLigneDto> Lignes);

public record UpdateBonLivraisonClientDto(
    int ClientId,
    int? DevisClientId,
    int? BonCommandeClientId,
    DateTime Date,
    decimal TotalTtc,
    string? Note,
    List<CreateBonLivraisonClientLigneDto> Lignes);

public record BonLivraisonClientListItemDto(
    int Id,
    string Numero,
    int ClientId,
    string ClientNom,
    DateTime Date,
    decimal TotalTtc,
    string? Note);
