using EventApp.Services.DTOs.Location;
using EventApp.Services.GuestService.GuestDtos;
using EventApp.Services.StaffService.StaffDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.LocationService
{
    public interface ILocationService
    {
        List<LocationDTO> GetLocations();
        List<LocationDTO> GetLocationsByName(String name);
        List<LocationDTO> GetLocationsBySize(int size);
        List<LocationDTO> GetLocationsByFee(int fee);
        int CreateLocation(LocationDTO locationDTO);
        void AddStaffLocation(List<StaffDTO> staffDTOs, int locationID);
    }
}
    