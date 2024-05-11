using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IAccountService
    {
        public UserIdentityDto GetUserIdentity(string userName, string password);
        public Task Create(UserRegisterDto request);
        public Task<UserDto> Get(int id);
        public Task Delete(int id);
    }
}
