using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.DTOs;

public record TiersDto(
    int Id,
    TypeTiers Type,
    string Nom,
    string? ICE,
    string? Adresse,
    string? Ville,
    string? Telephone,
    string? Email,
    string? ConditionsPaiement,
    bool Actif);

public record CreateTiersDto(
    TypeTiers Type,
    string Nom,
    string? ICE,
    string? Adresse,
    string? Ville,
    string? Telephone,
    string? Email,
    string? ConditionsPaiement,
    bool Actif = true);

public record UpdateTiersDto(
    TypeTiers Type,
    string Nom,
    string? ICE,
    string? Adresse,
    string? Ville,
    string? Telephone,
    string? Email,
    string? ConditionsPaiement,
    bool Actif);
