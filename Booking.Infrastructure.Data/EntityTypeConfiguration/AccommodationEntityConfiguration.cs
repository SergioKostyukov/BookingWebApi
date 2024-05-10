using Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.EntityTypeConfiguration
{
    internal class AccommodationEntityConfiguration : IEntityTypeConfiguration<Accommodation>
    {
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Hotel>()
                .WithMany()
                .HasForeignKey(x => x.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Type)
               .IsRequired()
               .HasConversion<int>();

            builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(1000);

            builder.Property(x => x.Price)
               .IsRequired()
               .HasColumnType("decimal(18, 2)");
        }
    }
}
