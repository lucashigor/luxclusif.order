using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.PostgreSql;
using luxclusif.order.kernel.Extensions;
using luxclusif.order.webapi.Extensions;
using luxclusif.order.webapi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace luxclusif.order.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
            var conf = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{env}.json");

            Configuration = conf.Build();
        }

        private string env;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (env != "Test")
            {
                services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection"), new PostgreSqlStorageOptions
                {
                    QueuePollInterval = TimeSpan.FromSeconds(10),
                }));
            }
            else
            {
                services.AddHangfire(config =>
                {
                    config.UseMemoryStorage();
                });
            }

            services.AddHangfireServer();

            services
                .AddDbContexts(Configuration)
                .AddUseCases()
                .AddGlobalExceptionHandlerMiddleware()
                .AddHttpContextAccessor()
                .AddControllers(options =>
                {
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => actionContext.GetErrorsModelState();
            });

            // Json settings
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "luxclusif.order.webapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "luxclusif.order.webapi v1"));
            }

            var options = new DashboardOptions
            {
                Authorization = new[] {
                    new DashboardAuthorization(new[]
                    {
                        new HangfireUserCredentials("user1",  "P@ssw0rd")
                    })
                }
            };

            app.UseHangfireDashboard("/hangfire", options);

            app.UseGlobalExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
