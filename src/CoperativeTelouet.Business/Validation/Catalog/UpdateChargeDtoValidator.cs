using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation.Catalog;

public class UpdateChargeDtoValidator : AbstractValidator<UpdateChargeDto>
{
    public UpdateChargeDtoValidator()
    {
        RuleFor(x => x.TypeChargeId)
            .GreaterThan(0).WithMessage("Le type de charge est obligatoire.");

        RuleFor(x => x.Libelle)
            .NotEmpty().WithMessage("Le libellé est obligatoire.")
            .MaximumLength(200).WithMessage("Le libellé ne doit pas dépasser 200 caractères.");

        RuleFor(x => x.MontantTtc)
            .GreaterThan(0).WithMessage("Le montant TTC doit être supérieur à zéro.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");
    }
}
