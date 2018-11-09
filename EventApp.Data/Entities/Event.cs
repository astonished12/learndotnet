﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal EstimatedBudget { get; set; }
        public string Description { get; set; }

        //ref to Location table one to one
        public int LocationId { get; set; }
        public Location Location { get; set; }

        //ref to EventTypes one to one
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        /*public ICollection<EventGuest> EventGuests { get; set; }*/
    }
}
