﻿using System;
using System.Collections.Generic;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Services.DTOs.Location;
using EventApp.Services.EventService.EventDtos;
using EventApp.Services.GuestService.GuestDtos;
using EventApp.Services.Utils;

namespace EventApp.Services.EventService
{
    public interface IEventService
    {
        List<EventDTO> GetEvents();
        List<EventDTO> GetEventsByName(String name);
        List<EventDTO> GetEventsByDate(DateTime dateTime);
        List<EventDTO> GetEventsByLocation(int locationId);
        List<EventDTO> GetEventsBySize(EventSize size);
        int CreateEvent(EventDTO evnt);
        void AddGuestsToEvent(List<GuestDTO> guests, int eventId);
        void ChangeEventLocation(LocationDTO newLocation, int eventId);

    }
}
