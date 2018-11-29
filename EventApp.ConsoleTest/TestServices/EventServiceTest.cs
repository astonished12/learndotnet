using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services;
using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;
using EventApp.Services.EventService.EventDtos;
using EventApp.Services.Services.EventService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventApp.ConsoleTest.MockEventService
{
    public class EventServiceTest
    {
        ServiceProvider services;

        public EventServiceTest(ServiceProvider services)
        {
            this.services = services;
        }

        public bool TestGetEventByName(string eventName)
        {
            using (var scope = services.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetService<IEventService>();

                var eventsAll = eventService.GetEvents();

                int x = eventService.CreateEvent(new EventDTO() { Name = eventName, Description = "Super descriere", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(2), EstimatedBudget = 2000, LocationId = 2, EventTypeId = 3 });
                var events = eventService.GetEventsByName(eventName);
                var eventsAll1 = eventService.GetEvents();

                EventDTO e = eventService.GetEventsByName(eventName)[0];

                return e.Name == eventName;

            }
        }

        public bool TestFindEventBySize()
        {
            using (var scope = services.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetService<IEventService>();

                var eventsBySize = eventService.GetEventsBySize(EventSize.T).Count;
                return eventsBySize == 2;
            }
        }
               
        public bool TestCreateEvent()
        {
            using (var scope = services.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetService<IEventService>();
                // nu trebuie legatura cu data =>> nou layer de test + add internal pe repository
                var eventRepo = scope.ServiceProvider.GetService<IRepository<Event>>();
                //
                EventDTO eventDTO = new EventDTO() { Description = "Sample description", StartTime = DateTime.Now, EstimatedBudget = 300, Name = "Super event", EventTypeId = 2, LocationId = 2 };

                int newEventId = eventService.CreateEvent(eventDTO);

                return newEventId == eventRepo.GetAll().Where(e => e.Id == newEventId).FirstOrDefault().Id;
            }
        }

        public bool TestAddGuestToEvent()
        {
            using (var scope = services.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetService<IEventService>();
                var eventRepo = scope.ServiceProvider.GetService<IRepository<Event>>();


                EventDTO eventDTO = new EventDTO() { Description = "Sample description", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(2), EstimatedBudget = 300, Name = "Test 1 eu si cosmin", EventTypeId = 2, LocationId = 2 };
                int newEventId = eventService.CreateEvent(eventDTO);

                GuestDTO guestDTO = new GuestDTO() { Id = 48, FirstName = "Ana", LastName = "Salacescu", Age = 38, Email = "anasalcescu@gmail.com", Phone = "0745600566" };
                GuestDTO guestDTO1 = new GuestDTO() { Id = 0, FirstName = "EUSICOSMIN", LastName = "FACEMUNTEST", Age = 38, Email = "smecherii@gmail.com", Phone = "0745600566" };
                List<GuestDTO> guestDTOs = new List<GuestDTO>() { guestDTO, guestDTO1 };

                eventService.AddGuestsToEvent(guestDTOs, newEventId);

                int numberOfGuestOfCreatedEvent = eventRepo.Query().Include(e => e.EventGuests).Where(e => e.Id == newEventId).FirstOrDefault().EventGuests.Count;

                return numberOfGuestOfCreatedEvent == 2;

            }

        }

        public bool TestChangeLocationOfEvent()
        {
            using (var scope = services.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetService<IEventService>();
                var eventRepo = scope.ServiceProvider.GetService<IRepository<Event>>();

                EventDTO eventDTO = new EventDTO() { Description = "Sample description", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(2), EstimatedBudget = 300, Name = "Super name", EventTypeId = 2, LocationId = 2 };
                int newEventId = eventService.CreateEvent(eventDTO);

                int oldLocationId = eventRepo.Query().Include(e => e.Location).Where(e => e.Id == newEventId).FirstOrDefault().LocationId;

                LocationDTO locationDTO = new LocationDTO() { Address = "Addresa mea", Capacity = 10, Name = "O noua locatie", RentFee = 1 };
                eventService.ChangeEventLocation(locationDTO, newEventId);

                int newLocationId = eventRepo.Query().Include(e => e.Location).Where(e => e.Id == newEventId).FirstOrDefault().LocationId;

                return newLocationId > oldLocationId;
            }
        }
    }
}
