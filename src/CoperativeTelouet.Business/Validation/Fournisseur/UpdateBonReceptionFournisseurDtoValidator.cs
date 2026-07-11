using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class UpdateBonReceptionFournisseurDtoValidator : AbstractValidator<UpdateBonReceptionFournisseurDto>
{
    public UpdateBonReceptionFournisseurDtoValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");

        RuleFor(x => x.TotalTtc)
            .GreaterThanOrEqualTo(0).WithMessage("Le total TTC doit être positif ou nul.");
    }
}
