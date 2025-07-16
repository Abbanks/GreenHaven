using GreenHaven.Domain.Common;

namespace GreenHaven.Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
