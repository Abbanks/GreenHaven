using GreenHaven.Domain.Common;

namespace GreenHaven.Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
