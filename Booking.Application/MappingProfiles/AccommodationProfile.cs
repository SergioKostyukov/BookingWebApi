using AutoMapper;
using Booking.Application.Dto;
using Booking.Core.Entities;

namespace Booking.Application.MappingProfiles;

public class AccommodationProfile : Profile
{
    public AccommodationProfile()
    {
        CreateMap<AccommodationAddDto, Accommodation>();
        CreateMap<AccommodationUpdateDto, Accommodation>();
        CreateMap<Accommodation, AccommodationDto>();
        CreateMap<Accommodation, AccommodationViewDto>();
    }
}
