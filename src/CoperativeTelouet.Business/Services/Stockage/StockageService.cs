using AutoMapper;
using CoperativeTelouet.Business.DTOs.Stockage;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Entities.Stockage;

namespace CoperativeTelouet.Business.Services.Stockage;

public class StockageService
    : GenericService<BonEntreeStockage, BonEntreeStockageDto, CreateBonEntreeStockageDto, UpdateBonEntreeStockageDto>
{
    public StockageService(IRepository<BonEntreeStockage> repo, IMapper mapper)
        : base(repo, mapper)
    {
    }

    // custom, non-generic operation — deposit flow needs the same transaction to:
    //  - generate NumeroLot (when EtatBac = Plein)
    //  - snapshot PrixParBacParJourApplique from AppSettings
    //  - compute BacsVides/BacsPleins Avant/Apres for the client
    //  - update StockBacsSociete (singleton)
    public Task<BonEntreeStockageDto> EnregistrerDepotAsync(
        CreateBonEntreeStockageDto dto,
        CancellationToken cancellationToken = default)
    {
        // TODO: implement full deposit workflow (NumeroLot generation, snapshots,
        // StockBacsSociete update) in the same transaction, then Mapper.Map<BonEntreeStockageDto>(entity).
        throw new NotImplementedException();
    }
}
