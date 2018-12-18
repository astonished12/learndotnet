using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.DTOs.Location;
using EventApp.Services.EventService.EventDtos;
using EventApp.Services.GuestService.GuestDtos;
using EventApp.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace EventApp.Services.EventService
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

        public EventDTO GetEventById(int id)
        {
            var eventFromId = eventRepo.GetById(id);
            if (eventFromId == null)
                return null;
            return new EventDTO().InjectFrom(eventFromId) as EventDTO;
        }

        public bool DeleteById(int id)
        {
            var eventFromId = eventRepo.GetById(id);
            if (eventFromId == null)
                return false;

            eventRepo.Delete(eventFromId);
            unitOfWork.Commit();
            return true;
        }

        public bool Update(EventDTO eventDTO)
        {
            var eventFromId = eventRepo.GetById(eventDTO.Id);
            if (eventFromId == null)
                return false;

            eventFromId.InjectFrom(eventDTO);
            eventRepo.Update(eventFromId);
            unitOfWork.Commit();
            return true;
        }

        public IEnumerable<EventDTO> GetEventsByName(String name)
        {
            return eventRepo.Query().Where(e => e.Name.ToLower().Contains(name)).ToList().Select(e => new EventDTO().InjectFrom(e) as EventDTO);
        }

        public List<EventDTO> GetEventsByDate(DateTime dateTime)
        {
            return eventRepo.Query().Where(e => e.StartTime.Equals(dateTime)).ToList().Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
        }

        public List<EventDTO> GetEventsByLocation(int locationId)
        {
            return eventRepo.Query().Where(e => e.Location.Id == locationId).ToList().Select(e => new EventDTO().InjectFrom(e) as EventDTO).ToList();
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

