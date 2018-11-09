using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class EventGuest
    {
        public bool HasAttended { get; set; }
        public bool ConfirmedAttendence { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

    }
}
