using EventApp.Data;
using EventApp.Data.Infrastructure;
using EventApp.Services.EventService;
using EventApp.Services.GuestService;
using EventApp.Services.ImageStorageService;
using EventApp.Services.LocationService;
using EventApp.Services.StatisticsService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Services.Infrastructure
{
    public static class DependencyMapper
    {
        public static IServiceCollection GetDependencies(IConfiguration configuration, IServiceCollection services)
        {
            services = Data.Infrastructure.DependencyMapper.GetDependencies(configuration, services);

            services.AddScoped<IEventService, EventService.EventService>();
            services.AddScoped<IGuestService, GuestService.GuestService>();
            services.AddScoped<ILocationService, LocationService.LocationService>();
            services.AddScoped<IStatisticsService, StatisticsService.StatisticsService>();
            services.AddScoped<IImageStorageService, ImageStorageService.ImageStorageService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
