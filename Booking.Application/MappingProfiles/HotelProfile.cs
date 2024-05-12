using AutoMapper;
using Booking.Application.Dto;
using Booking.Core.Entities;

namespace Booking.Application.MappingProfiles;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<HotelAddDto, Hotel>();
        CreateMap<HotelUpdateDto, Hotel>();
        CreateMap<Hotel, HotelDto>();
        CreateMap<Hotel, HotelViewDto>();
    }
}
