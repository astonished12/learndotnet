using EventApp.Data;
using EventApp.Data.Entities;
using EventApp.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventApp.Services
{
    public class TestService
    {
        private EventAppDataContext Context = new EventAppDataContext();
        // Ex3
        public IEnumerable<GuestDTO> GetMajorGuests()
        {
            return Context.Guests.Where(g => g.Age > 18)
                          .ToList()
                          .Select(g => (GuestDTO)new GuestDTO().InjectFrom(g))
                          .ToList();
        }

        // Ex4
        public IEnumerable<GuesNameDTO> GetFirstNameAndLastNameWithAgeBetween18And35OrderByFirstNameAsc()
        {
            return Context.Guests
                          .Where(g => g.Age >= 18 && g.Age <= 35)
                          .Select(g => (GuesNameDTO)new GuesNameDTO().InjectFrom(g))
                          .OrderBy(g => g.FirstName)
                          .ToList();
        }

        // Ex5
        public IEnumerable GetDetailsAboutFreeStaffs()
        {
            return Context.Staffs
                          .Where(s => s.LocationId == null)
                          .Select(s => new { s.FirstName, s.LastName, s.Email, s.Phone })
                          .ToList();
        }

        //Ex 6
        public IEnumerable GetLocationsFromIasi()
        {
            return Context.Locations
                          .Where(l => l.Address.Contains("Iasi"))
                          .ToList();
        }

        //Ex 7
        public IEnumerable<Staff> StaffTharEarnAtLeast1500()
        {
            return Context.Staffs
                          .Include(s => s.StaffRole)
                          .Where(s => (s.StaffRole.Name.Contains("DJ")
                                          || s.StaffRole.Name.Contains("Photographer")
                                          || s.StaffRole.Name.Contains("Performer"))
                                          && s.Fee >= 1500)
                          .ToList();
        }

        ////Ex 8
        //public IEnumerable GetGuestsDetailsFromExpertNetowrkParty()
        //{
        //    foreach (var eventGuest in Context.EventGuests.Include(eg => eg.Event).Where(eg => eg.Event.Name.Equals(.Include(eg => eg.Guest))
        //    {
        //        Console.WriteLine($"{eventGuest.Guest.FirstName} participate to {eventGuest.Event.Name}");
        //    }

        //}

    }
}
