using EventApp.Services;

namespace EventApp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestService testService = new TestService();
            foreach (var item in testService.GetMajorGuests())
            {
                System.Console.WriteLine(item.FirstName);
            }
        }
    }
}
