using Booking.Identity.Dto;

namespace Booking.Identity.Services
{
    public interface IIdentityService
    {
        public UserIdentityDto GetUserIdentity(string userName, string password);
        public Task Create(UserRegisterDto request);
        public string GenerateToken(UserIdentityDto request);
    }
}
