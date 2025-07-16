using GreenHaven.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHaven.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.FirstName)
                .HasMaxLength(100);

            builder.Property(au => au.LastName)
                .HasMaxLength(100);

            builder.Property(au => au.CreatedOn)
                  .IsRequired();

            builder.Property(au => au.ModifiedOn)
                .IsRequired(false);

            builder.Property(au => au.LastLoginDate)
            .IsRequired(false);

            builder.Property(au => au.ProfilePictureUrl)
                .HasMaxLength(2000);

            builder.Property(au => au.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

            builder.HasMany(au => au.Orders)
                .WithOne()
                .HasForeignKey(o => o.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(au => au.CartItems)
                .WithOne()
                .HasForeignKey(ci => ci.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(au => au.ProductReviews)
                .WithOne()
                .HasForeignKey(pr => pr.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(au => au.UserAddresses)
                .WithOne()
                .HasForeignKey(ua => ua.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(au => au.CreatedOn);
        }
    }
}
