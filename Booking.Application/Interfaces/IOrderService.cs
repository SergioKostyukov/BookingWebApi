using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderViewDto>> GetList(int userId);
        Task<List<OrderViewDto>> GetListOrdersToMe(int userId);
        Task<(OrderDto, List<OrderListDto>)> Get(int id);
        Task Add(OrderAddDto request);
        Task Confirm(OrderConfirmDto request);
        Task ConfirmPayment(int id);
        Task DeleteAccommodationFromOrder(int orderId, int accommodationId);
        Task Delete(int id);
    }
}
