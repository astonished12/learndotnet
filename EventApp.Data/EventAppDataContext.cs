using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data
{
    public class EventAppDataContext:DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventGuest> EventGuests { get; set; }

 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=GHOST;Initial Catalog=NLayerSample;User id=internship");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventGuest>().HasKey(eventGuest => new { eventGuest.EventId, eventGuest.GuestId });
        }

    }
}
