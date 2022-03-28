namespace luxclusif.order.domain.SeedWork;
public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? LastUpdateAt { get; protected set; }
    public DateTimeOffset? DeletedAt { get; protected set; }


    protected virtual void Validate()
    {
    }
}
