using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class EventGuest
    {
        public bool HasAttended { get; set; }
        public bool ConfirmedAttendence { get; set; }
        public decimal GiftAmount { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }

    }
}
