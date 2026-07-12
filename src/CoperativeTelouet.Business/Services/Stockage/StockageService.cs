using AutoMapper;
using CoperativeTelouet.Business.DTOs.Stockage;
using CoperativeTelouet.DataAccess.Repositories;
using CoperativeTelouet.Domain.Logging;
using CoperativeTelouet.Domain.Entities.Stockage;
using FluentValidation;

namespace CoperativeTelouet.Business.Services.Stockage;

public class StockageService
    : GenericService<BonEntreeStockage, BonEntreeStockageDto, CreateBonEntreeStockageDto, UpdateBonEntreeStockageDto>
{
    public StockageService(
        IRepository<BonEntreeStockage> repo,
        IMapper mapper,
        IEnumerable<IValidator<CreateBonEntreeStockageDto>> createValidators,
        IEnumerable<IValidator<UpdateBonEntreeStockageDto>> updateValidators,
        IErrorLogger logger)
        : base(repo, mapper, createValidators, updateValidators, logger)
    {
    }

    /// <summary>
    /// Deposit workflow: validate DTO shape, then (TODO) generate NumeroLot,
    /// snapshot rate, compute balance snapshots, update StockBacsSociete in one transaction.
    /// </summary>
    public async Task<BonEntreeStockageDto> EnregistrerDepotAsync(
        CreateBonEntreeStockageDto dto,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(CreateValidator, dto, cancellationToken);

        // TODO: implement full deposit workflow after shape validation.
        throw new NotImplementedException(
            "Implement NumeroLot, snapshots, and StockBacsSociete update in the same transaction.");
    }
}
