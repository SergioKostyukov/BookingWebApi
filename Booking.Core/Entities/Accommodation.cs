using Booking.Core.Enums;

namespace Booking.Core.Entities;

public class Accommodation
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccommodationType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
