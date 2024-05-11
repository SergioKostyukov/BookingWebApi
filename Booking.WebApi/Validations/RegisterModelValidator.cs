using Booking.WebApi.Models;
using FluentValidation;

namespace Booking.WebApi.Validations;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name field is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email field is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber field is required.")
            .Matches(@"^\+\d{12}$")
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password field is required.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Confirm password must match the password.");
    }
}
