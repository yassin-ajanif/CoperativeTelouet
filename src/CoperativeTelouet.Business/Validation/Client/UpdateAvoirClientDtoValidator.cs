using FluentValidation;
using CoperativeTelouet.Business.DTOs.Client;

namespace CoperativeTelouet.Business.Validation.Client;

public class UpdateAvoirClientDtoValidator : AbstractValidator<UpdateAvoirClientDto>
{
    public UpdateAvoirClientDtoValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La date est obligatoire.")
            .NotEqual(default(DateTime)).WithMessage("La date est obligatoire.");
    }
}
