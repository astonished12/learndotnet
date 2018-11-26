using EventApp.Data;
using EventApp.Data.Entities;
using EventApp.Services.DTOs.Event;
using EventApp.Services.DTOs.EventsGuest;
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

        private readonly EventAppDataContext Context;

        public TestService(EventAppDataContext context)
        {
            this.Context = context;
        }

        // Ex3
        public IEnumerable<GuestDTO> GetMajorGuests()
        {
            var guests = Context.Guests.Where(g => g.Age > 18).ToList();
            return guests.Select(g => new GuestDTO().InjectFrom(g) as GuestDTO);
        }

        // Ex4
        public IEnumerable<GuestNameDTO> GetFirstNameAndLastNameWithAgeBetween18And35OrderByFirstNameAsc()
        {
            return Context.Guests
                          .Where(g => g.Age >= 18 && g.Age <= 35)
                          .Select(g => new GuestNameDTO { FirstName = g.FirstName, LastName = g.LastName });
        }

        // Ex5
        public IEnumerable<StaffDetailDTO> GetDetailsAboutFreeStaffs()
        {
            return Context.Staffs.Where(s => s.LocationId == null)
                                        .Select(s => new StaffDetailDTO { FirstName = s.FirstName, LastName = s.LastName, Email = s.Email, Phone = s.Phone })
                                        .ToList();
        }

        //Ex 6
        public IEnumerable<LocationDTO> GetLocationsFromIasi()
        {
            var locations = Context.Locations.Where(l => l.Address.Contains("Iasi")).ToList();
            return locations.Select(s => new LocationDTO().InjectFrom(s) as LocationDTO).ToList();
        }

        //Ex 7
        public IEnumerable<StaffDTO> StaffTharEarnAtLeast1500()
        {
            var staffs = Context.Staffs
                          .Include(s => s.StaffRole)
                          .Where(s => (s.StaffRole.Name.Contains("DJ")
                                          || s.StaffRole.Name.Contains("Photographer")
                                          || s.StaffRole.Name.Contains("Performer"))
                                          && s.Fee >= 1500)
                          .ToList();

            return staffs.Select(s => new StaffDTO().InjectFrom(s) as StaffDTO).ToList();
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
                          .Select(eg => new GuestDetailDTO { FirstName = eg.Guest.FirstName, LastName = eg.Guest.LastName, Email = eg.Guest.Email })
                          .ToList();
        }

        //Ex 9
        public IEnumerable<GuestDTO> GetDetailsAboutMostGenerousGuestAtWeddings()
        {
            var guests = Context.EventGuests
                          .Include(eg => eg.Event)
                          .Include(eg => eg.Guest)
                          .Include(eg => eg.Event.EventType)
                          .Where(eg => eg.Event.EventType.Name.Equals("Wedding") && eg.HasAttended)
                          .OrderByDescending(eg => eg.GiftAmount)
                          .Take(5)
                          .ToList();

            return guests.Select(eg => new GuestDTO().InjectFrom(eg.Guest) as GuestDTO)
                         .ToList();

        }

        //Ex 10
        public IEnumerable<LocationDetailDTO> GetLocationDetailsWhichHasEventsNextYear()
        {
            return Context.Events
                           .Include(e => e.Location)
                           .Where(e => e.StartTime.Year > DateTime.Today.Year)
                           .Select(e => new LocationDetailDTO { Id = e.Location.Id, Address = e.Location.Address, Name = e.Location.Name })
                           .Distinct()
                           .ToList();

        }

        //Ex 11
        public IEnumerable<LocationDetailDTO> GetTop5BiggestLocations()
        {
            var locations = Context.EventGuests
                           .Include(eg => eg.Event)
                           .Include(eg => eg.Guest)
                           .Include(eg => eg.Event.Location)
                           .Where(eg => eg.HasAttended)
                           .OrderByDescending(eg => eg.Event.EventGuests.Count())
                           .ToList()
                           .GroupBy(eg => new
                           {
                               eg.Event.Location.Id,
                               eg.Event.Location.Name,
                               eg.Event.Location.Address
                           });

            return locations.Select(eg => new LocationDetailDTO { Id = eg.Key.Id, Name = eg.Key.Name, Address = eg.Key.Address }).Take(5).ToList();
        }

        //Ex 12 
        public IEnumerable<EventsGuestDTO> GetUniqueEmailsAndEventDetailsForWedding()
        {
            return Context.EventGuests.Include(eg => eg.Event)
                                      .Include(eg => eg.Guest)
                                      .Include(eg => eg.Event.EventType)
                                      .Where(eg => eg.Event.EventType.Name.Equals("Wedding") && eg.ConfirmedAttendence)
                                      .Select(eg => new EventsGuestDTO { Email = eg.Guest.Email, DescriptionEvent = eg.Event.Description, EventName = eg.Event.Name })
                                      .Distinct()
                                      .ToList();
        }

        //Ex 13
        public IEnumerable<LocationDetailDTO> GetFreeLocationInNextYearBetweenMaySeptember()
        {
            return Context.Events.Include(e => e.Location)
                                 .Where(e => e.StartTime.Year == 2019 && (e.StartTime.Month < 5 || e.StartTime.Month > 9))
                                 .Select(e => new LocationDetailDTO { Id = e.Location.Id, Address = e.Location.Address, Name = e.Location.Name })
                                 .Distinct()
                                 .ToList();
        }

        //Ex 14
        public int GetTheMonthOfTheYearThatHasMostEventsInIt()
        {
            var events2 = Context.Events.GroupBy(e => e.StartTime.Month).ToDictionary(x => x.Key, x => x.Count()).OrderByDescending(x => x.Value);
            var events = Context.Events.GroupBy(e => e.StartTime.Month)
                                        .Select(group => new
                                        {
                                            Month = group.Key,
                                            Count = group.Count()
                                        })
                                        .OrderByDescending(a => a.Count);

            return events.ToList().Take(1).Select(a => a.Month).FirstOrDefault();
        }

        //Ex 15
        public IEnumerable<StaffDTO> GetStaffDetailsThatWillWorkForBestWeddingEvent()
        {
            var staffs = Context.Events.Include(e => e.Location)
                                               .Include(e => e.Location.Staffs)
                                               .Where(e => e.Description.Contains("Best wedding ever"))
                                               .Select(e => e.Location.Staffs)
                                               .ToList().FirstOrDefault();

            return staffs.Select(s => new StaffDTO().InjectFrom(s) as StaffDTO);

        }

        //Ex 16
        public IEnumerable<StaffFeeDTO> GetMaxMinAvgForAllStaffRole()
        {
            return Context.Staffs.Include(s => s.StaffRole)
                                 .GroupBy(s => s.StaffRole.Name)
                                 .Select(group => new StaffFeeDTO
                                 {
                                     Name = group.Key,
                                     MaxFee = group.Min(s => s.Fee),
                                     MinFee = group.Max(s => s.Fee),
                                     AvgFee = group.Average(s => s.Fee)
                                 })
                                 .ToList();
        }

        //Ex 17
        public decimal TotalRentCostFromExpertNetworkChristmasParty()
        {
            return Context.Events.Include(e => e.Location)
                                         .Include(e => e.Location.Staffs)
                                         .Where(e => e.Name.Contains("Expert Network Christmas Party"))
                                         .Select(e => e.Location.Staffs.Sum(s => s.Fee) + e.Location.RentFee)
                                         .ToList().FirstOrDefault();

            //var events = Context.Events.Where(x => x.Name == "Expert Network Christmas Party").ToList();
            //var locationIds = events.Select(x => x.LocationId).ToList();
            //var locations = Context.Locations.Where(x => locationIds.Contains(x.Id)).ToList();
            //events.ForEach(even =>
            //{
            //    var currentLocation = locations.FirstOrDefault(x => x.Id == even.LocationId);
            //    if (currentLocation != null)
            //    {
            //        even.Location = currentLocation;
            //    }
            //});
        }

        //Ex 18
        public IEnumerable<ProfitWedding> ProfitForAllWeddings()
        {
            return Context.Events.Include(l => l.Location)
                            .Include(et => et.EventType)
                            .Include(s => s.Location.Staffs)
                            .Include(eg => eg.EventGuests)
                            .Where(evt => evt.Name.Contains("Wedding") && evt.EventGuests.Any(egh => egh.HasAttended))
                            .GroupBy(x => new { x.Name })
                            .Select(w => new ProfitWedding
                            {
                                WeddingName = w.Key.Name,
                                Profit = w.Sum(xw => xw.EventGuests.Where(ha => ha.HasAttended).Sum(ha => ha.GiftAmount))
                                - w.Select(lf => lf.Location.RentFee).FirstOrDefault()
                                - w.Select(sf => sf.Location.Staffs.Select(sfs => sfs.Fee).Sum()).FirstOrDefault()
                            })
                            .Distinct()
                            .ToList();

        }


        public IEnumerable<EventParticipant> CostEventPerParticipant()
        {
            return Context.Events.Include(e => e.Location)
                                 .Include(e => e.Location.Staffs)
                                 .Include(e => e.EventGuests)
                                 .Where(e => e.EventGuests.Any(egh => egh.HasAttended))
                                 .GroupBy(x => new { EventName = x.Name, x.Location.RentFee })
                                 .Select(g => new EventParticipant
                                 {
                                     EventName = g.Key.EventName,
                                     LocationName = g.Select(e => e.Location.Name).FirstOrDefault(),
                                     Cost = (g.Select(e => e.Location.Staffs.Select(sfs => sfs.Fee).Sum()).FirstOrDefault() + g.Key.RentFee) /
                                            (g.Select(e => e.Location.Staffs.Count()).FirstOrDefault() + g.Select(e => e.EventGuests.Where(eg => eg.HasAttended).Count()).FirstOrDefault())

                                 })
                                 .ToList();

        }
        



    } 
}
