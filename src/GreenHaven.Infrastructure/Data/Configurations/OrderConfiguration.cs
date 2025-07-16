using GreenHaven.Domain.Entities;
using GreenHaven.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHaven.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(o => o.OrderDate).IsRequired();

            builder.Property(o => o.ShippingAddressLine1).IsRequired().HasMaxLength(250);
            builder.Property(o => o.ShippingAddressLine2).HasMaxLength(250);
            builder.Property(o => o.ShippingCity).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ShippingState).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ShippingPostalCode).IsRequired().HasMaxLength(20);
            builder.Property(o => o.ShippingCountry).IsRequired().HasMaxLength(100);

            //builder.Property(o => o.StripePaymentIntentId).HasMaxLength(255); 
            //builder.Property(o => o.StripeChargeId).HasMaxLength(255); 
            //builder.Property(o => o.PaymentDate); 

            builder.HasOne<ApplicationUser>()
                   .WithMany(au => au.Orders)
                   .HasForeignKey(o => o.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
