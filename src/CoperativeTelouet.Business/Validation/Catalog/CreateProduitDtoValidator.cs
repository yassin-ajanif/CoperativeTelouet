using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation.Catalog;

public class CreateProduitDtoValidator : AbstractValidator<CreateProduitDto>
{
    public CreateProduitDtoValidator()
    {
        RuleFor(x => x.Reference)
            .NotEmpty().WithMessage("La référence est obligatoire.")
            .MaximumLength(50).WithMessage("La référence ne doit pas dépasser 50 caractères.");

        RuleFor(x => x.Designation)
            .NotEmpty().WithMessage("La désignation est obligatoire.")
            .MaximumLength(300).WithMessage("La désignation ne doit pas dépasser 300 caractères.");

        RuleFor(x => x.PrixAchatHT)
            .GreaterThanOrEqualTo(0).WithMessage("Le prix d'achat HT doit être positif ou nul.");

        RuleFor(x => x.PrixVenteHT)
            .GreaterThanOrEqualTo(0).WithMessage("Le prix de vente HT doit être positif ou nul.");

        RuleFor(x => x.TauxTVA)
            .GreaterThanOrEqualTo(0).WithMessage("Le taux de TVA doit être positif ou nul.");

        RuleFor(x => x.StockActuel)
            .GreaterThanOrEqualTo(0).WithMessage("Le stock actuel doit être positif ou nul.");

        RuleFor(x => x.StockMinimum)
            .GreaterThanOrEqualTo(0).WithMessage("Le stock minimum doit être positif ou nul.");
    }
}
