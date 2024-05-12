using Booking.WebApi.Models;
using FluentValidation;

namespace Booking.WebApi.Validations;

public class UpdateAccommodationModelValidator : AbstractValidator<UpdateAccommodationModel>
{
    public UpdateAccommodationModelValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id field is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name field is required.");

        RuleFor(x => x.Type)
            .NotEmpty().When(x => x.Type != 0)
            .WithMessage("Type field is required.")
            .IsInEnum()
            .WithMessage("Invalid AccommodationType.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description field is required.")
            .MaximumLength(1000)
            .WithMessage("Description must be shorter than 1000 characters.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Country field is required.")
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}
