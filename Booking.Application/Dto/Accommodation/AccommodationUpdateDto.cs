using Booking.Core.Enums;

namespace Booking.Application.Dto;

public record AccommodationUpdateDto(
    int Id,
    string Name,
    AccommodationType Type,
    string Description,
    decimal Price
);
