using EventApp.Data;
using EventApp.Data.Infrastructure;
using EventApp.Services.LocationServices;
using EventApp.Services.Services.EventService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Services.Infrastructure
{
    public class DependencyMapper
    {
        public static IServiceCollection GetDependencies(IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("EventAppConnectionString");

            var services = new ServiceCollection();
            services.AddDbContext<EventAppDataContext>(options => options.UseSqlServer(connection));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEventService, Services.EventService.EventService>();
            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<TestService, TestService>();

            return services;
        }
    }
}
