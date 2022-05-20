using luxclusif.order.application.Interfaces;
using luxclusif.order.infrastructure.Repositories.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace luxclusif.order.tests.Components.Utils
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return base.CreateHostBuilder()
                .UseEnvironment("Test");
        }

        public RabbitMqTestHelper rabbit;

        public CustomWebApplicationFactory()
        {
            rabbit = new RabbitMqTestHelper();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            Environment.SetEnvironmentVariable("ListOfAllowedAPIKeys", "ba64afaa614e49fca0e40153b95f2504");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<PrincipalContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<PrincipalContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTestDatabase");
                });


                var descriptorEvent = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(IMessageSenderInterface));


                if (descriptorEvent != null)
                {
                    services.Remove(descriptorEvent);
                }

                services.AddSingleton<IMessageSenderInterface>(rabbit);

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<PrincipalContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
