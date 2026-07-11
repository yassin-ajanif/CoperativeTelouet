using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.DTOs.Stockage;

public record BonSortieStockageDto(
    int Id,
    string Numero,
    int ClientId,
    DateTime DateSortie,
    EtatBac EtatBac,
    int NombreBacs,
    int? BonEntreeStockageId,
    int? FactureClientId,
    int BacsVidesAvant,
    int BacsVidesApres,
    int BacsPleinsAvant,
    int BacsPleinsApres,
    string? Note);

public record CreateBonSortieStockageDto(
    string Numero,
    int ClientId,
    DateTime DateSortie,
    EtatBac EtatBac,
    int NombreBacs,
    int? BonEntreeStockageId,
    int? FactureClientId,
    string? Note);

public record UpdateBonSortieStockageDto(string? Note);
