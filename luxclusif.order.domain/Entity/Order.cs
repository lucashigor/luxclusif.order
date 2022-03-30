using luxclusif.order.domain.SeedWork;
using luxclusif.order.domain.Validation;

namespace luxclusif.order.domain.Entity;
public class Order : AgregateRoot
{
    public string Name { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Value { get; private set; }

    public Order(string name, Guid userId, decimal value) : base ()
    {
        this.Name = name;
        this.UserId = userId;
        this.Value = value;

        this.Validate();
    }

    protected override void Validate()
    {
        Name.NotNullOrEmptyOrWhiteSpace();
        Name.BetweenLength(3, 100);

        UserId.NotNull();
        Value.NotNull();

        base.Validate();
    }
}
