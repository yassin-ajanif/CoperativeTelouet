namespace CoperativeTelouet.Business.DTOs;

public record ClientCompteLigneDto(
    DateTime Date,
    string Designation,
    string? Observation,
    decimal Debit,
    decimal Credit,
    decimal Solde);

public record ClientCompteDto(
    decimal SoldeActuel,
    IReadOnlyList<ClientCompteLigneDto> Lignes);
