using Booking.Core.Entities;
using Booking.Infrastructure.Data.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }
    public BookingDbContext() { }
    public DbSet<User> Users { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Accommodation> Accommodations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderList> OrdersLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new HotelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccommodationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderListEntityConfiguration());
    }
}
