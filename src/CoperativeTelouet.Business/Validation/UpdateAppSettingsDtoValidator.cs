using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation;

public class UpdateAppSettingsDtoValidator : AbstractValidator<UpdateAppSettingsDto>
{
    public UpdateAppSettingsDtoValidator()
    {
        RuleFor(x => x.DevisValiditeJoursDefaut)
            .GreaterThanOrEqualTo(0).WithMessage("La validité par défaut du devis doit être positive ou nulle.");

        RuleFor(x => x.BackupIntervalHours)
            .GreaterThan(0).WithMessage("L'intervalle de sauvegarde doit être supérieur à zéro.");

        RuleFor(x => x.BackupRetentionDays)
            .GreaterThanOrEqualTo(0).WithMessage("La rétention de sauvegarde doit être positive ou nulle.");

        RuleFor(x => x.PrixStockageParBacParJour)
            .GreaterThanOrEqualTo(0).WithMessage("Le prix de stockage par bac et par jour doit être positif ou nul.");
    }
}
