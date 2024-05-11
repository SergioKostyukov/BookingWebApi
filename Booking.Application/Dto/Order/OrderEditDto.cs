using Booking.Core.Enums;

namespace Booking.Application.Dto;

public class OrderEditDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal CheckAmount { get; set; }
    public string? Comment { get; set; }
}
