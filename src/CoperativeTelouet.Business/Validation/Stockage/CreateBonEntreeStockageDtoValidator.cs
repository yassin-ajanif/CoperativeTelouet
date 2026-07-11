using FluentValidation;
using CoperativeTelouet.Business.DTOs.Stockage;
using CoperativeTelouet.Domain.Enums;

namespace CoperativeTelouet.Business.Validation.Stockage;

public class CreateBonEntreeStockageDtoValidator : AbstractValidator<CreateBonEntreeStockageDto>
{
    public CreateBonEntreeStockageDtoValidator()
    {
        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Le numéro est obligatoire.")
            .MaximumLength(50).WithMessage("Le numéro ne doit pas dépasser 50 caractères.");

        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("Le client est obligatoire.");

        RuleFor(x => x.DateEntree)
            .NotEmpty().WithMessage("La date d'entrée est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date d'entrée est obligatoire.");

        RuleFor(x => x.EtatBac)
            .IsInEnum().WithMessage("L'état du bac n'est pas valide.");

        RuleFor(x => x.NombreBacs)
            .GreaterThan(0).WithMessage("Le nombre de bacs doit être supérieur à zéro.");

        When(x => x.EtatBac == EtatBac.Plein, () =>
        {
            RuleFor(x => x.ChambreFroideId)
                .NotNull().WithMessage("La chambre froide est obligatoire pour un bac plein.")
                .GreaterThan(0).WithMessage("La chambre froide est obligatoire pour un bac plein.");

            RuleFor(x => x.VarieteId)
                .NotNull().WithMessage("La variété est obligatoire pour un bac plein.")
                .GreaterThan(0).WithMessage("La variété est obligatoire pour un bac plein.");
        });

        When(x => x.EtatBac == EtatBac.Vide, () =>
        {
            RuleFor(x => x.ChambreFroideId)
                .Null().WithMessage("La chambre froide ne doit pas être renseignée pour un bac vide.");

            RuleFor(x => x.VarieteId)
                .Null().WithMessage("La variété ne doit pas être renseignée pour un bac vide.");
        });
    }
}
