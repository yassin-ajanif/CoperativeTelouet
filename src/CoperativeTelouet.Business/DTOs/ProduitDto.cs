namespace CoperativeTelouet.Business.DTOs;

public record ProduitDto(
    int Id,
    string Reference,
    string? CodeBarre,
    string Designation,
    string? Unite,
    decimal PrixAchatHT,
    decimal PrixVenteHT,
    decimal TauxTVA,
    decimal StockActuel,
    decimal StockMinimum,
    int? CategorieId,
    bool Actif,
    byte[]? ImageData);

public record CreateProduitDto(
    string Reference,
    string? CodeBarre,
    string Designation,
    string? Unite,
    decimal PrixAchatHT,
    decimal PrixVenteHT,
    decimal TauxTVA,
    decimal StockActuel,
    decimal StockMinimum,
    int? CategorieId,
    bool Actif = true,
    byte[]? ImageData = null);

public record UpdateProduitDto(
    string Reference,
    string? CodeBarre,
    string Designation,
    string? Unite,
    decimal PrixAchatHT,
    decimal PrixVenteHT,
    decimal TauxTVA,
    decimal StockActuel,
    decimal StockMinimum,
    int? CategorieId,
    bool Actif,
    byte[]? ImageData);
