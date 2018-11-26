using EventApp.Data.Entities;
using EventApp.Services.DTOs.Event;
using EventApp.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.ConsoleTest.MockEventService
{
    public class EventServiceSUT
    {
        ServiceProvider services;

        public EventServiceSUT(ServiceProvider services)
        {
            this.services = services;
        }

        public bool TestGetEventByName(string eventName)
        {
            using (var scope = services.CreateScope())
            {
                var eventServices = scope.ServiceProvider.GetService<IEventService>();

                var eventsAll = eventServices.GetEvents();

                eventServices.AddNewEvent(new EventDTO() { Name = eventName, Description = "Super descriere", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(2), EstimatedBudget = 2000, LocationId = 2, EventTypeId=3 });
                // var events = eventServices.GetEventsByName(eventName);
                var eventsAll1 = eventServices.GetEvents();

                EventDTO e = eventServices.GetEventsByName(eventName)[0];

                return e.Name == eventName;

            }
        }

    }
}
