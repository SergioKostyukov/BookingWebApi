using Booking.Core.Enums;

namespace Booking.WebApi.Models.Identity;

public class LoginModel
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
