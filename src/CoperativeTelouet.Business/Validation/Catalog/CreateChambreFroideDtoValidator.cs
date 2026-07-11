using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation.Catalog;

public class CreateChambreFroideDtoValidator : AbstractValidator<CreateChambreFroideDto>
{
    public CreateChambreFroideDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire.")
            .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères.");

        RuleFor(x => x.CapaciteBacs)
            .GreaterThan(0).WithMessage("La capacité en bacs doit être supérieure à zéro.");
    }
}
