using Booking.Core.Enums;

namespace Booking.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
