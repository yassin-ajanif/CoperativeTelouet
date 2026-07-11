using FluentValidation;
using CoperativeTelouet.Business.DTOs.Stockage;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.Validation.Stockage;

public class CreateBonSortieStockageDtoValidator : AbstractValidator<CreateBonSortieStockageDto>
{
    public CreateBonSortieStockageDtoValidator()
    {
        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Le numéro est obligatoire.")
            .MaximumLength(50).WithMessage("Le numéro ne doit pas dépasser 50 caractères.");

        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("Le client est obligatoire.");

        RuleFor(x => x.DateSortie)
            .NotEmpty().WithMessage("La date de sortie est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date de sortie est obligatoire.");

        RuleFor(x => x.EtatBac)
            .IsInEnum().WithMessage("L'état du bac n'est pas valide.");

        RuleFor(x => x.NombreBacs)
            .GreaterThan(0).WithMessage("Le nombre de bacs doit être supérieur à zéro.");

        When(x => x.EtatBac == EtatBac.Plein, () =>
        {
            RuleFor(x => x.BonEntreeStockageId)
                .NotNull().WithMessage("Le bon d'entrée est obligatoire pour un bac plein.")
                .GreaterThan(0).WithMessage("Le bon d'entrée est obligatoire pour un bac plein.");
        });

        When(x => x.EtatBac == EtatBac.Vide, () =>
        {
            RuleFor(x => x.BonEntreeStockageId)
                .Null().WithMessage("Le bon d'entrée ne doit pas être renseigné pour un bac vide.");

            RuleFor(x => x.FactureClientId)
                .Null().WithMessage("La facture client ne doit pas être renseignée pour un bac vide.");
        });
    }
}
