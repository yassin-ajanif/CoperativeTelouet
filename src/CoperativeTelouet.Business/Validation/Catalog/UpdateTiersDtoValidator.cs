using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation.Catalog;

public class UpdateTiersDtoValidator : AbstractValidator<UpdateTiersDto>
{
    public UpdateTiersDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire.")
            .MaximumLength(200).WithMessage("Le nom ne doit pas dépasser 200 caractères.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Le type de tiers n'est pas valide.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("L'adresse e-mail n'est pas valide.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        // ICE is optional: empty/null OK; if provided must be exactly 15 characters.
        RuleFor(x => x.ICE)
            .Must(ice => string.IsNullOrWhiteSpace(ice) || ice.Trim().Length == 15)
            .WithMessage("L'ICE peut être vide, sinon il doit comporter exactement 15 caractères.");
    }
}
