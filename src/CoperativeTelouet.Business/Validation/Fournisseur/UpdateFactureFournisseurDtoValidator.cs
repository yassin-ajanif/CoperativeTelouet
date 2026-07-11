using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class UpdateFactureFournisseurDtoValidator : AbstractValidator<UpdateFactureFournisseurDto>
{
    public UpdateFactureFournisseurDtoValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");

        RuleFor(x => x.RemiseGlobale)
            .GreaterThanOrEqualTo(0).WithMessage("La remise globale doit être positive ou nulle.");
    }
}
