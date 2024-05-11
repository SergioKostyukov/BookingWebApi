using Booking.Core.Enums;

namespace Booking.WebApi.Models;

public class UpdateAccommodationModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccommodationType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
