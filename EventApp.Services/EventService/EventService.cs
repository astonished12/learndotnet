using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;
using EventApp.Services.EventService.EventDtos;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace EventApp.Services.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Guest> guestRepo;
        private readonly IRepository<EventGuest> eventGuestRepository;

        public EventService(IRepository<Event> eventRepo, IUnitOfWork unitOfWork, IRepository<EventGuest> eventGuestRepository, IRepository<Guest> guestRepo)
        {
            this.eventRepo = eventRepo;
            this.unitOfWork = unitOfWork;
            this.guestRepo = guestRepo;
            this.eventGuestRepository = eventGuestRepository;
        }

        public List<EventDTO> GetEvents()
        {
            return eventRepo.GetAll().Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
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

        public List<EventDTO> GetEventsBySize(EventSize size)
        {
            var eventsBySize = eventGuestRepository.Query().Include(e => e.Event)
                                                   .Include(e => e.Guest)
                                                   .Where(e => e.HasAttended)
                                                   .GroupBy(e => e.Event)
                                                   .Where(g => g.Count() <= (int)size)
                                                   .ToList();


            return eventsBySize.Select(g => new EventDTO().InjectFrom(g.Key) as EventDTO)
                                               .ToList();
        }

        public int CreateEvent(EventDTO evnt)
        {
            Event newEvent = new Event().InjectFrom(evnt) as Event;
            eventRepo.Add(newEvent);
            unitOfWork.Commit();
            return newEvent.Id;
        }

        public void AddGuestsToEvent(List<GuestDTO> guests, int eventId)
        {
            var evnt = eventRepo.Query().Include(e => e.EventGuests).Where(e => e.Id == eventId).FirstOrDefault();
            if (evnt == null)
                return;

            foreach (var guestDTO in guests)
            {
                Guest guest;
                if (guestDTO.Id == 0)
                {
                    guest = new Guest().InjectFrom(guestDTO) as Guest;
                    guestRepo.Add(guest);
                    unitOfWork.Commit();
                }
                else
                {
                    guest = guestRepo.GetById(guestDTO.Id);
                }

                if (guest != null)
                {
                    evnt.EventGuests.Add(new EventGuest() { EventId = eventId, GuestId = guest.Id, HasAttended = true });
                }

            }

            eventRepo.Update(evnt);
            unitOfWork.Commit();
        }


        public void ChangeEventLocation(LocationDTO newLocation, int eventId)
        {

            var evnt = eventRepo.GetById(eventId);
            if (evnt != null)
            {
                evnt.Location = new Location().InjectFrom(newLocation) as Location;
                eventRepo.Update(evnt);
                unitOfWork.Commit();
            }

        }
    }
}

