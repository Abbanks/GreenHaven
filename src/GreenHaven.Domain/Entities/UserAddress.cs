using GreenHaven.Domain.Common;
using GreenHaven.Domain.Enums;

namespace GreenHaven.Domain.Entities
{
    public class UserAddress : AuditableEntity
    {
        public Guid UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; } = false;
        public AddressType AddressType { get; set; }
    }
}
