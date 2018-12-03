using System.Collections.Generic;
using EventApp.Services.DTOs.Location;
using EventApp.Services.EventService.EventDtos;
using EventApp.Services.GuestService.GuestDtos;
using EventApp.Services.StaffService.StaffDtos;

namespace EventApp.Services.TestService
{
    public interface ITestService
    {
        IEnumerable<EventParticipant> CostEventPerParticipant();
        IEnumerable<StaffDetailDTO> GetDetailsAboutFreeStaffs();
        IEnumerable<GuestDTO> GetDetailsAboutMostGenerousGuestAtWeddings();
        IEnumerable<GuestNameDTO> GetFirstNameAndLastNameWithAgeBetween18And35OrderByFirstNameAsc();
        IEnumerable<LocationDetailDTO> GetFreeLocationInNextYearBetweenMaySeptember();
        IEnumerable<GuestDetailDTO> GetGuestsDetailsFromExpertNetowrkParty();
        IEnumerable<LocationDetailDTO> GetLocationDetailsWhichHasEventsNextYear();
        IEnumerable<LocationDTO> GetLocationsFromIasi();
        IEnumerable<GuestDTO> GetMajorGuests();
        IEnumerable<StaffFeeDTO> GetMaxMinAvgForAllStaffRole();
        IEnumerable<StaffDTO> GetStaffDetailsThatWillWorkForBestWeddingEvent();
        int GetTheMonthOfTheYearThatHasMostEventsInIt();
        IEnumerable<LocationDetailDTO> GetTop5BiggestLocations();
        IEnumerable<EventsGuestDTO> GetUniqueEmailsAndEventDetailsForWedding();
        IEnumerable<ProfitWedding> ProfitForAllWeddings();
        IEnumerable<StaffDTO> StaffTharEarnAtLeast1500();
        decimal TotalRentCostFromExpertNetworkChristmasParty();
    }
}