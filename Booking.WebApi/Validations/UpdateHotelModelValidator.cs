using Booking.WebApi.Models;
using FluentValidation;

namespace Booking.WebApi.Validations;

public class UpdateHotelModelValidator : AbstractValidator<UpdateHotelModel>
{
    public UpdateHotelModelValidator()
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
            .WithMessage("Invalid HotelType.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description field is required.")
            .MaximumLength(1000)
            .WithMessage("Description must be shorter than 1000 characters.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country field is required.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City field is required.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address field is required.");
    }
}
