using System.IO;
using EventApp.Services;
using EventApp.Services.Infrastructure;
using EventApp.Services.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventApp.Data.Entities;
using EventApp.ConsoleTest.MockEventService;

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
            EventServiceSUT eventServiceSUT = new EventServiceSUT(services);
            
            if(eventServiceSUT.TestGetEventByName("Super Name"))
            {
                System.Console.WriteLine("Test one passed");
            }

        }
    }
}
