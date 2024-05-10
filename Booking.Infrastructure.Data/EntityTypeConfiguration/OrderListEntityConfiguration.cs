using Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.EntityTypeConfiguration
{
    internal class OrderListEntityConfiguration : IEntityTypeConfiguration<OrderList>
    {
        public void Configure(EntityTypeBuilder<OrderList> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.AccommodationId });

            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Accommodation>()
                .WithMany()
                .HasForeignKey(x => x.AccommodationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
