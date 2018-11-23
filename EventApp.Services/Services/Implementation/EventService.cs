using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.DTOs.Event;
using EventApp.Services.Services.Interfaces;

namespace EventApp.Services.Services.Implementation
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> eventRepository;

        public EventService(IRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public List<EventDTO> GetEvents()
        {
            var events = eventRepository.Query(_ => true);
            return new List<EventDTO>();
        }
    }
}
