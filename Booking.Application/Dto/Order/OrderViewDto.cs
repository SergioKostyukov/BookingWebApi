namespace Booking.Application.Dto;

public class OrderViewDto
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
