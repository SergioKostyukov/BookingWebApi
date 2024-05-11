using Booking.WebApi.Models;
using FluentValidation;

namespace Booking.WebApi.Validations;

public class OrderConfirmModelValidator : AbstractValidator<OrderConfirmModel>
{
    public OrderConfirmModelValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id field is required.");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("StartTime field is required.")
            .GreaterThan(DateTime.Now)
            .WithMessage("StartTime must be greater than current date.");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("EndTime field is required.")
            .GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be greater than StartTime.");

        RuleFor(x => x.PaymentType)
            .NotEmpty()
            .WithMessage("PaymentType field is required.")
            .IsInEnum()
            .WithMessage("Invalid PaymentType.");

        RuleFor(x => x.Comment)
            .MaximumLength(500)
            .WithMessage("Comment must be shorter than 500 characters.");
    }
}
