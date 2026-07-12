using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur.BonReception;

public class UpdateBonReceptionFournisseurDtoValidator : AbstractValidator<UpdateBonReceptionFournisseurDto>
{
    public UpdateBonReceptionFournisseurDtoValidator()
    {
        RuleFor(x => x.BonCommandeFournisseurId)
            .GreaterThan(0).WithMessage("Le bon de commande fournisseur est obligatoire.");

        RuleFor(x => x.FournisseurId)
            .GreaterThan(0).WithMessage("Le fournisseur est obligatoire.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");

        RuleFor(x => x.Lignes)
            .NotEmpty().WithMessage("Le bon de réception doit contenir au moins une ligne.");

        RuleForEach(x => x.Lignes)
            .SetValidator(new CreateBonReceptionFournisseurLigneDtoValidator());
    }
}
