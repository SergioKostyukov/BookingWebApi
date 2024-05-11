using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelViewDto>> GetList();
        Task<HotelDto> Get(int id);
        Task Add(HotelAddDto request);
        Task Delete(int id);
    }
}
