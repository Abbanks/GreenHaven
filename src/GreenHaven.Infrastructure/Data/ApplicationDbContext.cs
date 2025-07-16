using GreenHaven.Application.Common;
using GreenHaven.Domain.Entities;
using GreenHaven.Domain.Interfaces;
using GreenHaven.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenHaven.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(Expression.Convert(parameter, typeof(ISoftDelete)), nameof(ISoftDelete.IsDeprecated));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);
                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            builder.Entity<ApplicationUser>().HasQueryFilter(u => u.IsActive == true);
        }

        public override int SaveChanges()
        {
            ApplyAuditAndSoftDeleteChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditAndSoftDeleteChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditAndSoftDeleteChanges()
        {
            var currentUserId = _currentUserService.UserId;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditable auditableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.CreatedOn = DateTime.UtcNow;
                            auditableEntity.LastModifiedOn = null; 
                            auditableEntity.CreatedBy = currentUserId;
                            auditableEntity.LastModifiedBy = null;
                            break;

                        case EntityState.Modified:
                            auditableEntity.LastModifiedOn = DateTime.UtcNow;
                            auditableEntity.LastModifiedBy = currentUserId;
                            entry.Property(nameof(IAuditable.CreatedOn)).IsModified = false;
                            entry.Property(nameof(IAuditable.CreatedBy)).IsModified = false;
                            break;
                    }
                }

                if (entry.Entity is ApplicationUser userEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            userEntity.CreatedOn = DateTime.UtcNow;
                            userEntity.ModifiedOn = null; 
                            break;

                        case EntityState.Modified:
                            userEntity.ModifiedOn = DateTime.UtcNow;
                            entry.Property(nameof(ApplicationUser.CreatedOn)).IsModified = false;
                            break;
                    }
                }

                if (entry.Entity is ISoftDelete softDeleteEntity && entry.State == EntityState.Modified)
                {
                    if (entry.OriginalValues.GetValue<bool>(nameof(ISoftDelete.IsDeprecated)) == false &&
                        softDeleteEntity.IsDeprecated == true)
                    {
                        softDeleteEntity.DeletedOn = DateTime.UtcNow; 
                    }
                    else if (entry.OriginalValues.GetValue<bool>(nameof(ISoftDelete.IsDeprecated)) == true &&
                         softDeleteEntity.IsDeprecated == false)
                    {
                        softDeleteEntity.DeletedOn = null; 
                    }
                    else if (softDeleteEntity.IsDeprecated == entry.OriginalValues.GetValue<bool>(nameof(ISoftDelete.IsDeprecated)))
                    {
                        entry.Property(nameof(ISoftDelete.DeletedOn)).IsModified = false;
                    }
                }
            }
        }
    }
}