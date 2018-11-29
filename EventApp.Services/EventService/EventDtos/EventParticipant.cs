using System;

namespace EventApp.Services.EventService.EventDtos
{
    public class EventParticipant
    {
        public String EventName { get; set; }
        public String LocationName { get; set; }
        public decimal Cost { get; set; }
    }
}