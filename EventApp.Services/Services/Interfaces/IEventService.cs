using System;
using System.Collections.Generic;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Services.DTOs.Event;
using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;

namespace EventApp.Services.Services.Interfaces
{
    public interface IEventService
    {
        List<EventDTO> GetEvents();
        void AddNewEvent(EventDTO t);
        List<EventDTO> GetEventsByName(String name);
        List<EventDTO> GetEventsByDate(DateTime dateTime);
        List<EventDTO> GetEventsByLocation(int locationId);
        List<EventDTO> GetEventsBySize(int size);
        void CreateEvent(EventDTO evnt);
        void AddGuestsToEvent(List<GuestDTO> guests, int eventId);
        void ChangeEventLocation(LocationDTO newLocation, int eventId);

    }
}
