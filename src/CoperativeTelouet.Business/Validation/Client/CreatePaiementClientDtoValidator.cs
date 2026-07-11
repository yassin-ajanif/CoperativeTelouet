using FluentValidation;
using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Validation.Client;

public class CreatePaiementClientDtoValidator : AbstractValidator<CreatePaiementClientDto>
{
    public CreatePaiementClientDtoValidator()
    {
        RuleFor(x => x.FactureClientId)
            .GreaterThan(0).WithMessage("La facture client est obligatoire.");

        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Le mode de paiement n'est pas valide.");

        RuleFor(x => x.Montant)
            .GreaterThan(0).WithMessage("Le montant doit être supérieur à zéro.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");
    }
}
