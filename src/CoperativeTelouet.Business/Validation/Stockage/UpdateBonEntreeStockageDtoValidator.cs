using FluentValidation;
using CoperativeTelouet.Business.DTOs.Stockage;

namespace CoperativeTelouet.Business.Validation.Stockage;

public class UpdateBonEntreeStockageDtoValidator : AbstractValidator<UpdateBonEntreeStockageDto>
{
    public UpdateBonEntreeStockageDtoValidator()
    {
        RuleFor(x => x.Note)
            .MaximumLength(1000).WithMessage("La note ne doit pas dépasser 1000 caractères.")
            .When(x => x.Note is not null);
    }
}
