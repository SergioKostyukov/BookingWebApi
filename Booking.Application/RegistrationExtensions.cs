using Booking.Application.Interfaces;
using Booking.Application.MappingProfiles;
using Booking.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application;

public static class RegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(HotelProfile));
        services.AddAutoMapper(typeof(AccommodationProfile));
        services.AddAutoMapper(typeof(OrderProfile));

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IAccommodationService, AccommodationService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
