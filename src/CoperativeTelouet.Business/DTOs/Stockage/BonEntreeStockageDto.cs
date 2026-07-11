using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.DTOs.Stockage;

public record BonEntreeStockageDto(
    int Id,
    string Numero,
    int ClientId,
    DateTime DateEntree,
    EtatBac EtatBac,
    int? ChambreFroideId,
    int? VarieteId,
    string? NumeroLot,
    int NombreBacs,
    decimal? PrixParBacParJourApplique,
    int BacsVidesAvant,
    int BacsVidesApres,
    int BacsPleinsAvant,
    int BacsPleinsApres,
    string? Note);

public record CreateBonEntreeStockageDto(
    string Numero,
    int ClientId,
    DateTime DateEntree,
    EtatBac EtatBac,
    int? ChambreFroideId,
    int? VarieteId,
    int NombreBacs,
    string? Note);

public record UpdateBonEntreeStockageDto(string? Note);
