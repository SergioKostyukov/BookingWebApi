using AutoMapper;
using Booking.Application.Dto;
using Booking.Core.Entities;

namespace Booking.Application.MappingProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
