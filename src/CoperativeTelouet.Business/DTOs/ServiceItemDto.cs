namespace CoperativeTelouet.Business.DTOs;

public record ServiceItemDto(
    int Id,
    string Reference,
    string Designation,
    string? Unite,
    decimal PrixVenteHT,
    decimal CoutHT,
    decimal TauxTVA,
    bool Actif,
    string? Note,
    byte[]? ImageData);

public record CreateServiceItemDto(
    string Reference,
    string Designation,
    string? Unite,
    decimal PrixVenteHT,
    decimal CoutHT,
    decimal TauxTVA,
    bool Actif = true,
    string? Note = null,
    byte[]? ImageData = null);

public record UpdateServiceItemDto(
    string Reference,
    string Designation,
    string? Unite,
    decimal PrixVenteHT,
    decimal CoutHT,
    decimal TauxTVA,
    bool Actif,
    string? Note,
    byte[]? ImageData);
