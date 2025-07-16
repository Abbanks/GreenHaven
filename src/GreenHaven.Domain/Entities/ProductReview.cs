using GreenHaven.Domain.Common;

namespace GreenHaven.Domain.Entities
{
    public class ProductReview : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
