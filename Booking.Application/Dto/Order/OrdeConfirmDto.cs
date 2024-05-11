using Booking.Core.Enums;

namespace Booking.Application.Dto;

public class OrderConfirmDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? Comment { get; set; }
}
