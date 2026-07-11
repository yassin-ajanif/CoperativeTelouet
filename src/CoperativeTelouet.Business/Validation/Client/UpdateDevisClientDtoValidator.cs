using FluentValidation;
using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Validation.Client;

public class UpdateDevisClientDtoValidator : AbstractValidator<UpdateDevisClientDto>
{
    public UpdateDevisClientDtoValidator()
    {
        RuleFor(x => x.RemiseGlobale)
            .GreaterThanOrEqualTo(0).WithMessage("La remise globale doit être positive ou nulle.");
    }
}
