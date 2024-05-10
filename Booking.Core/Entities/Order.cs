using Booking.Core.Enums;

namespace Booking.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public PaymentType PaymentType { get; set; }
    public bool PaymentStatus { get; set; }
    public decimal CheckAmount { get; set; }
    public string? Comment { get; set; }
}
