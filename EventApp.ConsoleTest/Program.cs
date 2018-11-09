using EventApp.Data;
using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EventApp.ConsoleTest
{
    class Program
    {
        static EventAppDataContext context = new EventAppDataContext();

        ////select e.Name, l.Name, et.Name from Events e join Locations l on e.LocationId=l.Id join EventTypes et on e.EventTypeId=et.Id;
        //static void ShowEventNameLocationNameAndEventType()
        //{
        //    foreach (var evnt in context.Events.Include(eveniment => eveniment.Location).Include(eveniment => eveniment.EventType))
        //    {
        //        Console.WriteLine(evnt.Name + "#######" + evnt.Location.Name + "#######" + evnt.EventType.Name);
        //    }
        //}

        ////select * from EventGuests eg join Guests g on eg.GuestId=g.Id join Events e on e.Id=eg.EventId;
        //static void ShowGuestAndEvent()
        //{
        //    foreach (var guest in context.EventGuests.Include(e => e.Event).Include(g => g.Guest))
        //        Console.WriteLine(guest.Guest.FirstName + " is going to " + guest.Event.Name);
        //}


        static void FindEventsLocations()
        {
            foreach (var evnt in context.Events.Include(evnt => evnt.Location))
            {
                Console.WriteLine($"{evnt.Name} will be in {evnt.Location.Name}");
            }
        }

        static void FindEventLocation(int EventId)
        {
            Event evn = context.Events.Include(evnt => evnt.Location).FirstOrDefault(evnt => evnt.Id == EventId);
            if (evn != null)
                Console.WriteLine(evn.Location.Name);
        }

        static void FindEventsLocationsAndTypes()
        {
            foreach (var evnt in context.Events.Include(e => e.Location).Include(e => e.EventType))
            {
                Console.WriteLine($"{evnt.Name} will be in {evnt.Location.Name} and has type {evnt.EventType.Name}");
            }
        }

        static void FindEventLocationAndType(int EventId)
        {
            Event evnt = context.Events.Include(e => e.Location).Include(e=>e.EventType).FirstOrDefault(e => e.Id == EventId);
            if (evnt != null)
                Console.WriteLine($"{evnt.Name} will be in {evnt.Location.Name} and has type {evnt.EventType.Name}");
        }

        static void Main(string[] args)
        {
            //FindEventsLocations();
            //FindEventLocation(12);
            //FindEventsLocationsAndTypes();
            FindEventLocationAndType(2);
        }
    }
}
