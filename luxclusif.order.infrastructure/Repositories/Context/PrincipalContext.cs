using Microsoft.EntityFrameworkCore;
using DomainEntity = luxclusif.order.domain.Entity;

namespace luxclusif.order.infrastructure.Repositories.Context;
public class PrincipalContext : DbContext
{
    public PrincipalContext(DbContextOptions<PrincipalContext> options) : base(options)
    {

    }

    public DbSet<DomainEntity.Order> Order => Set<DomainEntity.Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<DomainEntity.Order>((v) => {
                v.HasKey(k => k.Id);
                v.Property(k => k.Name).HasMaxLength(100);
                v.Property(k => k.CreatedAt);
                v.Property(k => k.LastUpdateAt);
                v.Property(k => k.DeletedAt);
            });
    }
}
