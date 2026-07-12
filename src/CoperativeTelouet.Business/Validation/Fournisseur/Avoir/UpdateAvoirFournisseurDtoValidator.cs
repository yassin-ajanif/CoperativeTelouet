using FluentValidation;
using CoperativeTelouet.Business.DTOs.Fournisseur;

namespace CoperativeTelouet.Business.Validation.Fournisseur.Avoir;

public class UpdateAvoirFournisseurDtoValidator : AbstractValidator<UpdateAvoirFournisseurDto>
{
    public UpdateAvoirFournisseurDtoValidator()
    {
        RuleFor(x => x.FactureFournisseurId)
            .GreaterThan(0).WithMessage("La facture fournisseur est obligatoire.");

        RuleFor(x => x.FournisseurId)
            .GreaterThan(0).WithMessage("Le fournisseur est obligatoire.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");

        RuleFor(x => x.Lignes)
            .NotEmpty().WithMessage("L'avoir doit contenir au moins une ligne.");

        RuleForEach(x => x.Lignes)
            .SetValidator(new CreateAvoirFournisseurLigneDtoValidator());
    }
}
