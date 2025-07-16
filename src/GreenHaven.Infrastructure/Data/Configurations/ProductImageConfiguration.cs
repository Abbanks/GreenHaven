using GreenHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHaven.Infrastructure.Data.Configurations;
public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.Property(pi => pi.ImageUrl)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(pi => pi.AltText)
            .HasMaxLength(500);

        builder.Property(pi => pi.DisplayOrder)
            .IsRequired();

        builder.HasOne(pi => pi.Product)
               .WithMany(p => p.Images)
               .HasForeignKey(pi => pi.ProductId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(pi => new { pi.ProductId, pi.DisplayOrder });
    }
}
