using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure.Data;

public static class RegistrationExtensions
{
    public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BookingDbContext>(options =>
        {
            options.UseSqlServer(
                configuration["ConnectionStrings:DefaultConnectionString"],
                options =>
                {
                    options.MigrationsAssembly(typeof(BookingDbContext).Assembly.FullName);
                });
        });
    }
}
