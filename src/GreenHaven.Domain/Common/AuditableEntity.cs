using GreenHaven.Domain.Interfaces;

namespace GreenHaven.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity, IAuditable, ISoftDelete
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public bool IsDeprecated { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }
}
