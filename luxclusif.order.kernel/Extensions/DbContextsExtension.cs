using luxclusif.order.application.Interfaces;
using luxclusif.order.domain.Repository;
using luxclusif.order.infrastructure;
using luxclusif.order.infrastructure.Repositories;
using luxclusif.order.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace luxclusif.order.kernel.Extensions;
public static class DbContextsExtension
{
    public static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("PrincipalDatabase");


        if (!string.IsNullOrEmpty(conn))
        {
            services.AddDbContext<PrincipalContext>(
                options => options.UseNpgsql(conn));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        return services;
    }
}
