using EventApp.Services;

namespace EventApp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestService testService = new TestService();
            foreach (var item in testService.CostEventPerParticipant())
            {
                System.Console.WriteLine($"{item.Cost} {item.EventName} {item.LocationName}");
            }
            //System.Console.WriteLine(testService.GetTheMonthOfTheYearThatHasMostEventsInIt());

        }
    }
}
