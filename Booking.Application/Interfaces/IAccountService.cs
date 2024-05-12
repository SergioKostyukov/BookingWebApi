using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<UserDto> Get(int id);
        public Task Delete(int id);
    }
}
