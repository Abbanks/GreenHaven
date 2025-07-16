using GreenHaven.Domain.Common;

namespace GreenHaven.Domain.Entities
{
    public class CartItem : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
