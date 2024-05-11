using Booking.Core.Enums;

namespace Booking.Application.Dto;

public class UserIdentityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
