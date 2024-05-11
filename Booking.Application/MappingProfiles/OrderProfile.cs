using AutoMapper;
using Booking.Application.Dto;
using Booking.Core.Entities;

namespace Booking.Application.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderViewDto>();

        CreateMap<OrderList, OrderListDto>();
    }
}
