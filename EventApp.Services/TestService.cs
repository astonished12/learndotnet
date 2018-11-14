using EventApp.Data;
using EventApp.Data.Entities;
using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;
using EventApp.Services.DTOs.Staff;
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
                          .Select(g => new GuestDTO().InjectFrom(g) as GuestDTO)
                          .ToList();
        }

        // Ex4
        public IEnumerable<GuestNameDTO> GetFirstNameAndLastNameWithAgeBetween18And35OrderByFirstNameAsc()
        {
            return Context.Guests
                          .Where(g => g.Age >= 18 && g.Age <= 35)
                          .Select(g => new GuestNameDTO().InjectFrom(g) as GuestNameDTO)
                          .OrderBy(g => g.FirstName)
                          .ToList();
        }

        // Ex5
        public IEnumerable<StaffDetailDTO> GetDetailsAboutFreeStaffs()
        {
            return Context.Staffs
                          .Where(s => s.LocationId == null)
                          .ToList()
                          .Select(s => new StaffDetailDTO().InjectFrom(s) as StaffDetailDTO)
                          .ToList();
        }

        //Ex 6
        public IEnumerable<LocationDTO> GetLocationsFromIasi()
        {
            return Context.Locations
                          .Where(l => l.Address.Contains("Iasi"))
                          .ToList()
                          .Select(s => new LocationDTO().InjectFrom(s) as LocationDTO)
                          .ToList();
        }

        //Ex 7
        public IEnumerable<StaffDTO> StaffTharEarnAtLeast1500()
        {
            return Context.Staffs
                          .Include(s => s.StaffRole)
                          .Where(s => (s.StaffRole.Name.Contains("DJ")
                                          || s.StaffRole.Name.Contains("Photographer")
                                          || s.StaffRole.Name.Contains("Performer"))
                                          && s.Fee >= 1500)
                          .ToList()
                          .Select(s => new StaffDTO().InjectFrom(s) as StaffDTO)
                          .ToList();
        }

        //Ex 8
        public IEnumerable<GuestDetailDTO> GetGuestsDetailsFromExpertNetowrkParty()
        {
            return Context.EventGuests
                          .Include(eg => eg.Event)
                          .Include(eg => eg.Guest)
                          .Where(eg => eg.Event.Name.Equals("Expert Network Christmas Party"))
                          .OrderBy(eg => eg.Guest.FirstName)
                          .ThenBy(eg => eg.Guest.LastName)
                          .ToList()
                          .Select(eg => new GuestDetailDTO().InjectFrom(eg.Guest) as GuestDetailDTO)
                          .ToList();


        }

    }
}
