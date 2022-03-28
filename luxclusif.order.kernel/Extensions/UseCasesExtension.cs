using luxclusif.order.application.Interfaces;
using luxclusif.order.application.Models;
using luxclusif.order.application.UseCases.Order.CreateOrder;
using luxclusif.order.infrastructure.rabbitmq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace luxclusif.order.kernel.Extensions;
public static class UseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var assembly1 = Assembly.GetExecutingAssembly();
        Assembly configurationAppAssembly = typeof(CreateOrder).Assembly;

        services.AddMediatR(assembly1,configurationAppAssembly);

        services.AddSingleton<IMessageSenderInterface, SendMessageRabbitmq>();

        services.AddScoped<Notifier>();

        return services;
    }
}
