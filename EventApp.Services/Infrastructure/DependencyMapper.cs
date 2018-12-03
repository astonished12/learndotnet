using EventApp.Data;
using EventApp.Data.Infrastructure;
using EventApp.Services.EventService;
using EventApp.Services.LocationService;
using EventApp.Services.TestService;
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
            services.AddScoped<IEventService, EventService.EventService>();
            services.AddScoped<ILocationService, LocationService.LocationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITestService, TestService.TestService>();

            return services;
        }
    }
}
