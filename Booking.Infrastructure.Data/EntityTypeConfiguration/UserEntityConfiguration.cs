using Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.EntityTypeConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15);

            builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.Role)
               .IsRequired()
               .HasConversion<int>();

            builder.Property(x => x.Password)
               .IsRequired();

            builder.Property(x => x.CreatedDate)
               .IsRequired();
        }
    }
}
