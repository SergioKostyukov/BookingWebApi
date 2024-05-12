using AutoMapper;
using Booking.Identity.Dto;
using Booking.Core.Entities;

namespace Booking.Identity.MappingProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserIdentityDto>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
