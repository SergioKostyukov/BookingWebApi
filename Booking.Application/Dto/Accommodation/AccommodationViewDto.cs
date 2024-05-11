using Booking.Core.Enums;

namespace Booking.Application.Dto;

public class AccommodationViewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccommodationType Type { get; set; }
    public decimal Price { get; set; }
}
