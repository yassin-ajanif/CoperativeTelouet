using FluentValidation;
using CoperativeTelouet.Business.DTOs;

namespace CoperativeTelouet.Business.Validation.Catalog;

public class UpdateCategorieDtoValidator : AbstractValidator<UpdateCategorieDto>
{
    public UpdateCategorieDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire.")
            .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères.");
    }
}
