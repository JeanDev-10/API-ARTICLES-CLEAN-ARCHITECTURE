namespace Articles.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

    protected void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
