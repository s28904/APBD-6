using FluentValidation;

namespace apbd_6.Validators;

public static class Validators
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateAnimalUpdateRequestValidator>();
    }
}