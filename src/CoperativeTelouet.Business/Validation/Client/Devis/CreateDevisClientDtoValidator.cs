using FluentValidation;
using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Validation.Client.Devis;

public class CreateDevisClientDtoValidator : AbstractValidator<CreateDevisClientDto>
{
    public CreateDevisClientDtoValidator()
    {
        RuleFor(x => x.Numero)
            .MaximumLength(50).WithMessage("Le numéro est trop long.");

        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("Le client est obligatoire.");

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
            .SetValidator(new CreateDevisClientLigneDtoValidator());
    }
}
