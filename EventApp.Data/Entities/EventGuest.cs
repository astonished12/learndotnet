using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class EventGuest
    {
        public int EventId { get; set; }
        public int GuestId { get; set; }

        public bool HasAttended { get; set; }
        public bool ConfirmedAttendence { get; set; }

        public Event Event { get; set; }
        public Guest Guest { get; set; }

    }
}
