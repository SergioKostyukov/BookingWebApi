using Booking.Core.Enums;

namespace Booking.Application.Dto;

public record HotelUpdateDto(
    int Id,
    string Name,
    HotelType Type,
    string Description,
    string Country,
    string City,
    string Address
);
