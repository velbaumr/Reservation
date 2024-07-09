using FluentValidation;
using Services.Dtos;

namespace WebApi.Validators;
public class ReservationValidator : AbstractValidator<ReservationDto>
{
    public ReservationValidator()
    {
        RuleFor(x => x.From)
            .GreaterThanOrEqualTo(DateTime.Today);
        RuleFor(x => x.To)
            .GreaterThanOrEqualTo(DateTime.Today.AddDays(1))
            .GreaterThanOrEqualTo(x => x.From.AddDays(1));
    }
}