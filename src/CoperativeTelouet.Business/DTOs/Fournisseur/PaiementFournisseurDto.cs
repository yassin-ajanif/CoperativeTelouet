using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.DTOs.Fournisseur;

public record PaiementFournisseurDto(
    int Id,
    int FactureFournisseurId,
    ModePaiement Mode,
    decimal Montant,
    DateTime Date,
    string? Reference);

public record CreatePaiementFournisseurDto(
    int FactureFournisseurId,
    ModePaiement Mode,
    decimal Montant,
    DateTime Date,
    string? Reference);

public record UpdatePaiementFournisseurDto(
    ModePaiement Mode,
    decimal Montant,
    DateTime Date,
    string? Reference);
