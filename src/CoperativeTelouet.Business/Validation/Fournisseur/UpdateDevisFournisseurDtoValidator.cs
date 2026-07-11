using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class UpdateDevisFournisseurDtoValidator : AbstractValidator<UpdateDevisFournisseurDto>
{
    public UpdateDevisFournisseurDtoValidator()
    {
        RuleFor(x => x.RemiseGlobale)
            .GreaterThanOrEqualTo(0).WithMessage("La remise globale doit être positive ou nulle.");
    }
}
