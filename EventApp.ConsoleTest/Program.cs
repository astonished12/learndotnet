using EventApp.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventApp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EventAppDataContext();

            //Get event relation with location and eventype
            //foreach (var evnt in context.Events.Include(eveniment => eveniment.Location).Include(eveniment => eveniment.EventType))
            //{
            //    Console.WriteLine(evnt.Name + "#######" + evnt.Location.Name + "#######" + evnt.EventType.Name);
            //}

            foreach (var guest in context.EventGuests.Include(e => e.Event).Include(g => g.Guest))
                Console.WriteLine(guest.Guest.FirstName + " la " + guest.Event.Name);

        }
    }
}
