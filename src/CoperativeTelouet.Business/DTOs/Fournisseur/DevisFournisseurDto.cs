namespace CoperativeTelouet.Business.DTOs.Fournisseur;

public record DevisFournisseurLigneDto(
    int Id,
    int DevisFournisseurId,
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record CreateDevisFournisseurLigneDto(
    int? ProduitId,
    int? ServiceId,
    string Designation,
    decimal Quantite,
    decimal PrixUnitaireHT,
    decimal Remise,
    decimal TauxTVA,
    string? Conditionnement);

public record DevisFournisseurConditionDto(int Id, int DevisFournisseurId, string Titre, string Valeur, int Ordre);

public record CreateDevisFournisseurConditionDto(string Titre, string Valeur, int Ordre);

public record DevisFournisseurDto(
    int Id,
    string Numero,
    int FournisseurId,
    DateTime Date,
    DateTime DateValidite,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<DevisFournisseurLigneDto> Lignes,
    List<DevisFournisseurConditionDto> Conditions);

public record CreateDevisFournisseurDto(
    string Numero,
    int FournisseurId,
    DateTime Date,
    DateTime DateValidite,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<CreateDevisFournisseurLigneDto> Lignes,
    List<CreateDevisFournisseurConditionDto>? Conditions = null);

public record UpdateDevisFournisseurDto(
    int FournisseurId,
    DateTime Date,
    DateTime DateValidite,
    decimal RemiseGlobale,
    decimal TotalTtc,
    string? Note,
    List<CreateDevisFournisseurLigneDto> Lignes);

public record DevisFournisseurListItemDto(
    int Id,
    string Numero,
    int FournisseurId,
    string FournisseurNom,
    DateTime Date,
    DateTime DateValidite,
    decimal TotalTtc,
    string? Note);
