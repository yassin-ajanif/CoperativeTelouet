namespace CoperativeTelouet.Business.DTOs;

public record FournisseurCompteLigneDto(
    DateTime Date,
    string Designation,
    string? Observation,
    decimal Debit,
    decimal Credit,
    decimal Solde);

public record FournisseurCompteDto(
    decimal SoldeActuel,
    IReadOnlyList<FournisseurCompteLigneDto> Lignes);
