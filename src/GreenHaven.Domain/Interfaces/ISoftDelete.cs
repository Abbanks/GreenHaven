namespace GreenHaven.Domain.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeprecated { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
