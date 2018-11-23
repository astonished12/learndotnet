using System;
using System.Collections.Generic;
using System.Text;
using EventApp.Services.DTOs.Event;

namespace EventApp.Services.Services.Interfaces
{
    public interface IEventService
    {
        List<EventDTO> GetEvents();
    }
}
