using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class CreateDevisFournisseurDtoValidator : AbstractValidator<CreateDevisFournisseurDto>
{
    public CreateDevisFournisseurDtoValidator()
    {
        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Le numéro est obligatoire.");

        RuleFor(x => x.FournisseurId)
            .GreaterThan(0).WithMessage("Le fournisseur est obligatoire.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");

        RuleFor(x => x.DateValidite)
            .NotEmpty().WithMessage("La date de validité est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date de validité est obligatoire.");

        RuleFor(x => x.RemiseGlobale)
            .GreaterThanOrEqualTo(0).WithMessage("La remise globale doit être positive ou nulle.");

        RuleFor(x => x.Lignes)
            .NotEmpty().WithMessage("Le devis doit contenir au moins une ligne.");

        RuleForEach(x => x.Lignes)
            .SetValidator(new CreateDevisFournisseurLigneDtoValidator());
    }
}
