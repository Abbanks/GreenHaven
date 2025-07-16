using GreenHaven.Domain.Entities;
using GreenHaven.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHaven.Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(ua => ua.AddressLine1)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(ua => ua.AddressLine2)
                .HasMaxLength(250);

            builder.Property(ua => ua.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ua => ua.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ua => ua.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(ua => ua.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ua => ua.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(ua => ua.AddressType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(ua => ua.IsDefault)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne<ApplicationUser>()
                   .WithMany(au => au.UserAddresses)
                   .HasForeignKey(ua => ua.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ua => ua.UserId);
            builder.HasIndex(ua => new { ua.UserId, ua.AddressType }).IsUnique();
            builder.HasIndex(ua => new { ua.UserId, ua.IsDefault });
        }
    }
}
