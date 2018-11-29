using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.DTOs.Location;
using EventApp.Services.DTOs.Staff;
using EventApp.Services.LocationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventApp.ConsoleTest.TestServices
{
    public class LocationServiceTest
    {
        ServiceProvider services;

        public LocationServiceTest(ServiceProvider services)
        {
            this.services = services;
        }

        public bool TestCreateLocation()
        {
            using (var scope = services.CreateScope())
            {
                var locationService = scope.ServiceProvider.GetService<ILocationService>();
                var locationRepo = scope.ServiceProvider.GetService<IRepository<Location>>();

                LocationDTO locationDTO = new LocationDTO() { Name = "Locuinta mea de iarna", Address = "E la tara", Capacity = 2, RentFee = 2200 };
                int newLocationId = locationService.CreateLocation(locationDTO);

                return newLocationId == locationRepo.GetAll().Where(l => l.Id == newLocationId).FirstOrDefault().Id;
            }

        }

        public bool TestAddStaffToLocation()
        {
            using (var scope = services.CreateScope())
            {
                var locationService = scope.ServiceProvider.GetService<ILocationService>();
                var locationRepo = scope.ServiceProvider.GetService<IRepository<Location>>();

                LocationDTO locationDTO = new LocationDTO() { Name = "Test location", Address = "E la tara", Capacity = 2, RentFee = 2200 };
                int locationId = locationService.CreateLocation(locationDTO);
                StaffDTO staffDTO = new StaffDTO() { Id = 1059, FirstName = "Cosmin1", LastName = "Popescu", Email = "cosminpopescu@gmail.com", Phone = "0745600566", Fee = 1500, StaffRoleId = 2 };
                StaffDTO staffDTO1 = new StaffDTO() { Id = 0, FirstName = "Becks", LastName = "LA1L", Email = "beckslasticla@gmail.com", Phone = "0789898989", Fee = 8, StaffRoleId = 3 };
                List<StaffDTO> staffDTOs = new List<StaffDTO>() { staffDTO, staffDTO1 };

                locationService.AddStaffLocation(staffDTOs, locationId);

                int numberOfTotalStaffOfNewLocation = locationRepo.Query().Include(l => l.Staffs).Where(l => l.Id == locationId).FirstOrDefault().Staffs.Count;
                return numberOfTotalStaffOfNewLocation == 2;
            }
        }
    }
}
