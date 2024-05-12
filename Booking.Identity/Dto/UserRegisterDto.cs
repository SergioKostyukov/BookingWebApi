using Booking.Core.Enums;

namespace Booking.Identity.Dto;

public class UserRegisterDto
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string Password { get; set; } = string.Empty;
}
