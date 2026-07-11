using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class CreatePaiementFournisseurDtoValidator : AbstractValidator<CreatePaiementFournisseurDto>
{
    public CreatePaiementFournisseurDtoValidator()
    {
        RuleFor(x => x.FactureFournisseurId)
            .GreaterThan(0).WithMessage("La facture fournisseur est obligatoire.");

        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Le mode de paiement n'est pas valide.");

        RuleFor(x => x.Montant)
            .GreaterThan(0).WithMessage("Le montant doit être supérieur à zéro.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");
    }
}
