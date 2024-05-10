using Booking.Core.Enums;

namespace Booking.Core.Entities;

public class Hotel
{
    public int Id { get; set; }
    public int ManagerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public HotelType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public float Rating { get; set; }
}
