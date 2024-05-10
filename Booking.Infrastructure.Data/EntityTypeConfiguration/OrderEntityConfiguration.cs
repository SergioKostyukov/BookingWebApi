using Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.EntityTypeConfiguration
{
    internal class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Hotel>()
                .WithMany()
                .HasForeignKey(x => x.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreateDate)
               .IsRequired();

            builder.Property(x => x.StartTime)
               .IsRequired();

            builder.Property(x => x.EndTime)
               .IsRequired();

            builder.Property(x => x.PaymentType)
               .IsRequired()
               .HasConversion<int>();

            builder.Property(x => x.PaymentStatus)
               .IsRequired();

            builder.Property(x => x.CheckAmount)
               .IsRequired()
               .HasColumnType("decimal(18, 2)");

            builder.Property(x => x.Comment)
                .HasMaxLength(500);
        }
    }
}
