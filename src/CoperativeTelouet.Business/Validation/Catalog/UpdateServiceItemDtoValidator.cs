using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation.Catalog;

public class UpdateServiceItemDtoValidator : AbstractValidator<UpdateServiceItemDto>
{
    public UpdateServiceItemDtoValidator()
    {
        RuleFor(x => x.Reference)
            .NotEmpty().WithMessage("La référence est obligatoire.");

        RuleFor(x => x.Designation)
            .NotEmpty().WithMessage("La désignation est obligatoire.");

        RuleFor(x => x.PrixVenteHT)
            .GreaterThanOrEqualTo(0).WithMessage("Le prix de vente HT doit être positif ou nul.");

        RuleFor(x => x.CoutHT)
            .GreaterThanOrEqualTo(0).WithMessage("Le coût HT doit être positif ou nul.");

        RuleFor(x => x.TauxTVA)
            .GreaterThanOrEqualTo(0).WithMessage("Le taux de TVA doit être positif ou nul.");
    }
}
