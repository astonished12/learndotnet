using EventApp.Services;

namespace EventApp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestService testService = new TestService();
            foreach (var item in testService.GetUniqueEmailsAndEventDetailsForWedding())
            {
                System.Console.WriteLine($"{item.Email} si {item.EventName} si {item.DescriptionEvent}");
            }


        }
    }
}
