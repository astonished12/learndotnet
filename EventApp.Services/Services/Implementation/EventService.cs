using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.DTOs.Event;
using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;
using EventApp.Services.Services.Interfaces;
using Omu.ValueInjecter;

namespace EventApp.Services.Services.Implementation
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Guest> guestRepo;


        public EventService(IRepository<Event> eventRepo, IUnitOfWork unitOfWork, IRepository<Guest> guestRepo)
        {
            this.eventRepo = eventRepo;
            this.unitOfWork = unitOfWork;
            this.guestRepo = guestRepo;
        }

        public List<EventDTO> GetEvents()
        {
            return eventRepo.GetAll().Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
        }

        public void AddNewEvent(EventDTO evnt)
        {
            eventRepo.Add(new Event().InjectFrom(evnt) as Event);
            unitOfWork.Commit();

        }

        public List<EventDTO> GetEventsByName(String name)
        {
            return eventRepo.Query().Where(e => e.Name.Contains(name)).Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
        }

        public List<EventDTO> GetEventsByDate(DateTime dateTime)
        {
            return eventRepo.Query().Where(e => e.StartTime.Equals(dateTime)).Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
        }

        public List<EventDTO> GetEventsByLocation(int locationID)
        {
            return eventRepo.Query().Where(e => e.Location.Id == locationID).Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
        }

        public List<EventDTO> GetEventsBySize(int size)
        {
            return eventRepo.Query().Where(e => e.Location.Capacity == size).Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
        }

        public void CreateEvent(EventDTO evnt)
        {
            eventRepo.Add(new Event().InjectFrom(evnt) as Event);
        }

        public void AddGuestsToEvent(List<GuestDTO> guests, int eventId)
        {
            var evnt = eventRepo.GetById(eventId);
            if (evnt != null)
            {
                foreach (var guest in guests)
                {
                    var validGuest = guestRepo.GetById(guest.Id);
                    if (validGuest != null)
                    {
                        evnt.EventGuests.Add(new EventGuest() { EventId = eventId, GuestId = validGuest.Id, HasAttended = true });
                    }
                }

                eventRepo.Update(evnt);
                unitOfWork.Commit();
            }
        }

        public void ChangeEventLocation(LocationDTO newLocation, int eventId)
        {

            var evnt = eventRepo.GetById(eventId);

            if (evnt != null)
            {
                evnt.Location = new Location().InjectFrom(newLocation) as Location;
            }

            eventRepo.Update(evnt);
            unitOfWork.Commit();

        }
    }
}
