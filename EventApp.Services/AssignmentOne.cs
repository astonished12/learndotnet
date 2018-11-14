using EventApp.Data;
using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EventApp.ConsoleTest
{
    public static class AssignmentOne
    {
        static EventAppDataContext Context = new EventAppDataContext();

        public static void FindEventsLocations()
        {
            foreach (var evnt in Context.Events.Include(evnt => evnt.Location))
            {
                Console.WriteLine($"{evnt.Name} will be in {evnt.Location.Name}");
            }
        }

        public static void FindEventLocation(int EventId)
        {
            Event evn = Context.Events.Include(evnt => evnt.Location).FirstOrDefault(evnt => evnt.Id == EventId);
            if (evn != null)
                Console.WriteLine(evn.Location.Name);
        }

        public static void FindEventsLocationsAndTypes()
        {
            foreach (var evnt in Context.Events.Include(e => e.Location).Include(e => e.EventType))
            {
                Console.WriteLine($"{evnt.Name} will be in {evnt.Location.Name} and has type {evnt.EventType.Name}");
            }
        }

        public static void FindEventLocationAndType(int EventId)
        {
            Event evnt = Context.Events.Include(e => e.Location).Include(e => e.EventType).FirstOrDefault(e => e.Id == EventId);
            if (evnt != null)
                Console.WriteLine($"{evnt.Name} will be in {evnt.Location.Name} and has type {evnt.EventType.Name}");
        }

        public static void DisplayEventGuests()
        {
            foreach (var eventGuest in Context.EventGuests.Include(eg => eg.Event).Include(eg => eg.Guest))
            {
                Console.WriteLine($"{eventGuest.Guest.FirstName} participate to {eventGuest.Event.Name}");
            }
        }

        public static void DisplayEventGuestFromEvent()
        {
            foreach (var eg in Context.Events)
            {
                Console.WriteLine($"{eg.Name}:\n");
                foreach (var g in Context.EventGuests.Include(e => e.Guest).Include(e => e.Event).Where(e => e.EventId == eg.Id))
                    Console.WriteLine($"{g.Guest.FirstName} {g.Guest.LastName}");
                Console.WriteLine(Environment.NewLine);
            }
        }

        public static void DisplayLocationEmployees()
        {
            foreach (var location in Context.Locations.Include(location => location.Staffs))
            {
                Console.WriteLine($"Locatia : {location.Name}");
                foreach (var employee in location.Staffs)
                {
                    Console.WriteLine($"{employee.FirstName}  {employee.LastName}");
                }
                Console.WriteLine(Environment.NewLine);
            }
        }

        public static void DisplayLocationStaff(int LocationId)
        {
            Console.WriteLine(Context.Locations.Include(l => l.Staffs).FirstOrDefault(l => l.Id == LocationId).Staffs.Count);
        }

        public static void DisplayLocations()
        {
            foreach (var evnt in Context.Events)
            {
                Console.WriteLine($"{evnt.Name} si {evnt.Location.Name}");
            }
        }

        public static void ExerciseThree()
        {
            var majorGuests = Context.Guests
                                     .Where(g => g.Age >= 18)
                                     .ToList();
                                   

        }
    }
}