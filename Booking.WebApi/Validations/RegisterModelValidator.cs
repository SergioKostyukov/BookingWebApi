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

        RuleFor(x => x.Role)
            .NotEmpty().When(x => x.Role != 0)
            .WithMessage("Role field is required.")
            .IsInEnum()
            .WithMessage("Invalid UserRole.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber field is required.")
            .Matches(@"^\+\d{12}$")
            .WithMessage("Invalid phone number format. (Correct from is +012345678901)");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password field is required.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Confirm password must match the password.");
    }
}
