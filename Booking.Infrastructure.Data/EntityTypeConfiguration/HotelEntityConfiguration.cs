using Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.EntityTypeConfiguration
{
    internal class HotelEntityConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name)
               .IsRequired();

            builder.Property(x => x.Type)
               .IsRequired()
               .HasConversion<int>();

            builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(1000);

            builder.Property(x => x.Country)
               .IsRequired();

            builder.Property(x => x.City)
               .IsRequired();
            
            builder.Property(x => x.Address)
               .IsRequired();
            
            builder.Property(x => x.Rating)
               .IsRequired();
        }
    }
}
