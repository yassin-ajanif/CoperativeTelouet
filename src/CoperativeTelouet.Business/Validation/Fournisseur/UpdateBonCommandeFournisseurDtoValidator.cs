using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur;

public class UpdateBonCommandeFournisseurDtoValidator : AbstractValidator<UpdateBonCommandeFournisseurDto>
{
    public UpdateBonCommandeFournisseurDtoValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");
    }
}
