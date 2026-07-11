using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation;

public class UpdateStockBacsSocieteDtoValidator : AbstractValidator<UpdateStockBacsSocieteDto>
{
    public UpdateStockBacsSocieteDtoValidator()
    {
        RuleFor(x => x.TotalBacsOriginal)
            .GreaterThanOrEqualTo(0).WithMessage("Le total de bacs original doit être positif ou nul.");
    }
}
