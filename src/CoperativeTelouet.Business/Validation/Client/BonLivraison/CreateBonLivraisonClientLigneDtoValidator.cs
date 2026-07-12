using FluentValidation;
using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Validation.Client.BonLivraison;

public class CreateBonLivraisonClientLigneDtoValidator : AbstractValidator<CreateBonLivraisonClientLigneDto>
{
    public CreateBonLivraisonClientLigneDtoValidator()
    {
        RuleFor(x => x.Designation)
            .NotEmpty().WithMessage("La désignation est obligatoire.");

        RuleFor(x => x.QuantiteLivree)
            .GreaterThanOrEqualTo(0).WithMessage("La quantité livrée doit être positive ou nulle.");

        RuleFor(x => x.PrixUnitaireHT)
            .GreaterThanOrEqualTo(0).WithMessage("Le prix unitaire HT doit être positif ou nul.");

        RuleFor(x => x.Remise)
            .GreaterThanOrEqualTo(0).WithMessage("La remise doit être positive ou nulle.");

        RuleFor(x => x.TauxTVA)
            .GreaterThanOrEqualTo(0).WithMessage("Le taux de TVA doit être positif ou nul.");
    }
}
