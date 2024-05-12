using Booking.Core.Enums;

namespace Booking.Identity.Dto;

public record UserIdentityDto(
    int Id,
    string Name,
    string Email,
    UserRole Role
);
