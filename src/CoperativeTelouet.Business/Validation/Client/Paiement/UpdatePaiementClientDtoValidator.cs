using FluentValidation;
using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Validation.Client.Paiement;

public class UpdatePaiementClientDtoValidator : AbstractValidator<UpdatePaiementClientDto>
{
    public UpdatePaiementClientDtoValidator()
    {
        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Le mode de paiement n'est pas valide.");

        RuleFor(x => x.Montant)
            .GreaterThan(0).WithMessage("Le montant doit être supérieur à zéro.");
    }
}
