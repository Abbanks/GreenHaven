using GreenHaven.Domain.Entities;
using GreenHaven.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenHaven.Infrastructure.Data.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.Property(pr => pr.Rating)
                .IsRequired();

            builder.Property(pr => pr.Comment)
                .HasMaxLength(1000);

            builder.HasOne(pr => pr.Product)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(pr => pr.ProductId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<ApplicationUser>()
                   .WithMany(au => au.ProductReviews)
                   .HasForeignKey(pr => pr.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pr => new { pr.ProductId, pr.UserId }).IsUnique();
            builder.HasIndex(pr => pr.CreatedOn);
        }
    }
}
