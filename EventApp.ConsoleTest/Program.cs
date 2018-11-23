using System.IO;
using EventApp.Services;
using EventApp.Services.Infrastructure;
using EventApp.Services.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.ConsoleTest
{
    class Program
    {
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        static void Main(string[] args)
        {
            var services = DependencyMapper.GetDependencies(GetConfiguration()).BuildServiceProvider();
            using (var scope = services.CreateScope())
            {
                var eventServices = scope.ServiceProvider.GetService<IEventService>();
                var events = eventServices.GetEvents();
            }
        }
    }
}
