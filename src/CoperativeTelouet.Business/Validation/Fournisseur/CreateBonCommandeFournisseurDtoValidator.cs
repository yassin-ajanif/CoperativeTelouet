using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class CreateBonCommandeFournisseurDtoValidator : AbstractValidator<CreateBonCommandeFournisseurDto>
{
    public CreateBonCommandeFournisseurDtoValidator()
    {
        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Le numéro est obligatoire.");

        RuleFor(x => x.FournisseurId)
            .GreaterThan(0).WithMessage("Le fournisseur est obligatoire.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");

        RuleFor(x => x.Lignes)
            .NotEmpty().WithMessage("Le bon de commande doit contenir au moins une ligne.");

        RuleForEach(x => x.Lignes)
            .SetValidator(new CreateBonCommandeFournisseurLigneDtoValidator());
    }
}
