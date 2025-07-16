using GreenHaven.Domain.Entities;
using GreenHaven.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHaven.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(ci => ci.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(ci => ci.ProductName).HasMaxLength(250);
            builder.Property(ci => ci.UnitPrice).HasColumnType("decimal(18,2)");

            builder.HasOne<ApplicationUser>()
                   .WithMany(au => au.CartItems)
                   .HasForeignKey(ci => ci.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Product)
                   .WithMany(p => p.CartItems)
                   .HasForeignKey(ci => ci.ProductId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ci => new { ci.UserId, ci.ProductId }).IsUnique();

            builder.HasIndex(ci => ci.UserId);
        }
    }
}
