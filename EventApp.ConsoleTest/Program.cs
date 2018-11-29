using System.IO;
using EventApp.Services;
using EventApp.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventApp.Data.Entities;
using EventApp.ConsoleTest.MockEventService;
using EventApp.ConsoleTest.TestServices;

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

        private static void EventServicesTests(ServiceProvider services)
        {
            EventServiceTest eventServiceSUT = new EventServiceTest(services);

            if (eventServiceSUT.TestGetEventByName("Super Name"))
            {
                System.Console.WriteLine("Test Get Event By Name passed");
            }

            if (eventServiceSUT.TestFindEventBySize())
            {
                System.Console.WriteLine("Test Find Event By Size passed");
            }

            if (eventServiceSUT.TestCreateEvent())
            {
                System.Console.WriteLine("Test create event passed");
            }

            if (eventServiceSUT.TestAddGuestToEvent())
            {
                System.Console.WriteLine("Test add guess to event passed");
            }

            if (eventServiceSUT.TestChangeLocationOfEvent())
            {
                System.Console.WriteLine("Change event location test passed");
            }

        }

        private static void LocationServiceTests(ServiceProvider serivces)
        {
            LocationServiceTest locationServiceTests = new LocationServiceTest(serivces);
            if (locationServiceTests.TestAddStaffToLocation())
            {
                System.Console.WriteLine("Add staff to location test PASSED");
            }
        }
        static void Main(string[] args)
        {
            var services = DependencyMapper.GetDependencies(GetConfiguration()).BuildServiceProvider();
            //EventServicesTests(services);
            LocationServiceTests(services);

        }
    }
}
