using Booking.Identity.Dto;
using Microsoft.AspNetCore.Identity.Data;

namespace Booking.Identity.Services
{
    public interface IIdentityService
    {
        public Task Register(UserRegisterDto request);
        public Task<UserIdentityDto> GetUserIdentity(string userName, string password);
        public string GenerateToken(UserIdentityDto request);
    }
}
