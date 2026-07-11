using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.DTOs.Client;

public record PaiementClientDto(
    int Id,
    int FactureClientId,
    ModePaiement Mode,
    decimal Montant,
    DateTime Date,
    string? Reference);

public record CreatePaiementClientDto(
    int FactureClientId,
    ModePaiement Mode,
    decimal Montant,
    DateTime Date,
    string? Reference);

public record UpdatePaiementClientDto(
    ModePaiement Mode,
    decimal Montant,
    DateTime Date,
    string? Reference);
