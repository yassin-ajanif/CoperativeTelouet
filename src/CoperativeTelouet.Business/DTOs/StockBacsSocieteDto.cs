namespace CoperativeTelouet.Business.DTOs;

public record StockBacsSocieteDto(
    int Id,
    int BacsVides,
    int BacsPleins,
    int TotalBacsOriginal);

public record UpdateStockBacsSocieteDto(
    int BacsVides,
    int BacsPleins,
    int TotalBacsOriginal);
