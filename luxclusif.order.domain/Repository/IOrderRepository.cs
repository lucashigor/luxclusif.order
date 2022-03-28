using luxclusif.order.domain.Entity;
using luxclusif.order.domain.SeedWork;

namespace luxclusif.order.domain.Repository;
public interface IOrderRepository : IRepository
{
    public Task Insert(Order user, CancellationToken cancellationToken);
}
