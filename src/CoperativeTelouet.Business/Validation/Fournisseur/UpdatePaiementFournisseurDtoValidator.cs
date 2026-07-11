using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class UpdatePaiementFournisseurDtoValidator : AbstractValidator<UpdatePaiementFournisseurDto>
{
    public UpdatePaiementFournisseurDtoValidator()
    {
        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Le mode de paiement n'est pas valide.");

        RuleFor(x => x.Montant)
            .GreaterThan(0).WithMessage("Le montant doit être supérieur à zéro.");
    }
}
