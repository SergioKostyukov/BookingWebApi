using Booking.Core.Enums;

namespace Booking.WebApi.Models;

public class OrderConfirmModel
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? Comment { get; set; }
}
