using apbd_6.DTOs;
using FluentValidation;

namespace apbd_6.Validators;

public class CreateAnimalUpdateRequestValidator : AbstractValidator<ReplaceAnimalRequest>
{
    public CreateAnimalUpdateRequestValidator()
    {
        RuleFor(e => e.Name).MaximumLength(200).NotNull();
        RuleFor(e => e.Description).MaximumLength(200);
        RuleFor(e => e.Category).MaximumLength(200).NotNull();
        RuleFor(e => e.Area).MaximumLength(200).NotNull();
    }
    
}