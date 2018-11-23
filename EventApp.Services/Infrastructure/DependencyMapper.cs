using System;
using System.Collections.Generic;
using System.Text;
using EventApp.Data;
using EventApp.Data.Infrastructure;
using EventApp.Services.Services.Implementation;
using EventApp.Services.Services.Interfaces;
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
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
