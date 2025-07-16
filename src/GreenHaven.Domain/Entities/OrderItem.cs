using GreenHaven.Domain.Common;

namespace GreenHaven.Domain.Entities
{
    public class OrderItem : AuditableEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
