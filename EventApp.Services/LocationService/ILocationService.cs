using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;
using EventApp.Services.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.LocationServices
{
    public interface ILocationService
    {
        List<GuestDTO> GetLocations();
        List<GuestDTO> GetLocationsByName(String name);
        List<GuestDTO> GetLocationsBySize(int size);
        List<GuestDTO> GetLocationsByFee(int fee);
        int CreateLocation(LocationDTO locationDTO);
        void AddStaffLocation(List<StaffDTO> staffDTOs, int locationID);
    }
}
