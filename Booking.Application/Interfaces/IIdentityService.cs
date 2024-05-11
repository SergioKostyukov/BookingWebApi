using Booking.Application.Dto;

namespace Booking.Application.Interfaces
{
    public interface IIdentityService
    {
        public string GenerateToken(UserIdentityDto request);
    }
}
