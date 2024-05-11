using Booking.Core.Enums;

namespace Booking.Application.Dto;

public class AccommodationAddDto
{
    public int HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccommodationType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
