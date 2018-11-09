﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public short Capacity { get; set; }
        public decimal RentFee { get; set; }

        //one to many (1 location has multiple events) ref to Event table
        public ICollection<Event> Events { get; set; }
    }
}
