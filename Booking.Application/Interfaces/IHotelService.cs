using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelViewDto>> GetList();
        Task<List<HotelViewDto>> GetListByUserId(int userId);
        Task<HotelDto> Get(int id);
        Task Add(HotelAddDto request);
        Task Update(HotelUpdateDto request);
        Task Delete(int id);
    }
}
