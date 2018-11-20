using System;

namespace EventApp.Services.DTOs.Event
{
    public class EventParticipant
    {
        public String EventName { get; set; }
        public String LocationName { get; set; }
        public decimal Cost { get; set; }
    }
}