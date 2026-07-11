namespace CoperativeTelouet.Business.DTOs;

public record ChargeDto(
    int Id,
    int TypeChargeId,
    DateTime Date,
    string Libelle,
    int? FournisseurId,
    string? BeneficiaireLibre,
    decimal MontantTtc,
    string? Note);

public record CreateChargeDto(
    int TypeChargeId,
    DateTime Date,
    string Libelle,
    int? FournisseurId,
    string? BeneficiaireLibre,
    decimal MontantTtc,
    string? Note);

public record UpdateChargeDto(
    int TypeChargeId,
    DateTime Date,
    string Libelle,
    int? FournisseurId,
    string? BeneficiaireLibre,
    decimal MontantTtc,
    string? Note);
