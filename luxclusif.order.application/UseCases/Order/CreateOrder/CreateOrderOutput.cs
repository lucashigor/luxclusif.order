using DomainEntity = luxclusif.order.domain.Entity;
namespace luxclusif.order.application.UseCases.Order.CreateOrder;
public class CreateOrderOutput
{

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastUpdateAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public CreateOrderOutput(Guid id, Guid userId,string name, decimal value,DateTimeOffset createdAt, DateTimeOffset? lastUpdateAt, DateTimeOffset? deletedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        LastUpdateAt = lastUpdateAt;
        DeletedAt = deletedAt;
        UserId = userId;
        Value = value;
    }

    public static CreateOrderOutput FromUser(DomainEntity.Order entity)
    => new(
            entity.Id,
            entity.UserId,
            entity.Name,
            entity.Value,
            entity.CreatedAt,
            entity.LastUpdateAt,
            entity.DeletedAt
            );
}
