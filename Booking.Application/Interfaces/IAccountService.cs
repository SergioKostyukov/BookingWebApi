using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IAccountService
    {
        public UserIdentityDto GetUserIdentity(string userName, string password);
        public Task Create(UserRegisterDto request);
    }
}
