using luxclusif.order.domain.Repository;
using luxclusif.order.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using DomainEntity = luxclusif.order.domain.Entity;

namespace luxclusif.order.infrastructure.Repositories;
public class OrderRepository
    : IOrderRepository
{
    private readonly PrincipalContext context;
    private DbSet<DomainEntity.Order> users => context.Set<DomainEntity.Order>();

    public OrderRepository(PrincipalContext context)
    => this.context = context;


    public async Task Insert(DomainEntity.Order user, CancellationToken cancellationToken)
    => await users.AddAsync(user, cancellationToken);
}
