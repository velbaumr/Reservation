using FluentValidation;
using Services.Dtos;

namespace WebApi.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .MinimumLength(2);
        
        RuleFor(x => x.LastName)
            .NotNull()
            .MinimumLength(2);

        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.IdCode)
            .NotNull()
            .Length(11);
    }
}