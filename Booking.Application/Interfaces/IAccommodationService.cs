using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IAccommodationService
    {
        Task<List<AccommodationViewDto>> GetList(int hotelId);
        Task<AccommodationDto> Get(int id);
        Task Add(AccommodationAddDto request, int numder);
        Task Delete(int id);
    }
}
