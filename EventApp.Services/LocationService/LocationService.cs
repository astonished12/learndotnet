using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.DTOs.Guest;
using EventApp.Services.DTOs.Location;
using EventApp.Services.DTOs.Staff;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace EventApp.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork unitOfWork;
        private IRepository<Location> locationRepo;
        private IRepository<Staff> staffRepo;

        public LocationService(IRepository<Location> locationRepo, IRepository<Staff> staffRepo, IUnitOfWork unitOfWork)
        {
            this.locationRepo = locationRepo;
            this.staffRepo = staffRepo;
            this.unitOfWork = unitOfWork;
        }

        public void AddStaffLocation(List<StaffDTO> staffDTOs, int locationID)
        {
            var location = locationRepo.Query().Include(l => l.Staffs).Where(l => l.Id == locationID).FirstOrDefault();
            if (location == null)
                return;

            foreach (var staffDTO in staffDTOs)
            {
                Staff staff;
                if (staffDTO.Id == 0)
                {
                    staff = new Staff().InjectFrom(staffDTO) as Staff;
                    staffRepo.Add(staff);
                    unitOfWork.Commit();
                }
                else
                {
                    staff = staffRepo.GetById(staffDTO.Id);
                }

                var isValidStaffRoleId = staffRepo.GetById(staff.StaffRoleId) != null ? true : false;
                if (staff != null && isValidStaffRoleId)
                {
                    location.Staffs.Add(staff);
                }

            }

            locationRepo.Update(location);
            unitOfWork.Commit();
        }

        public int CreateLocation(LocationDTO locationDTO)
        {
            Location newLocation = new Location().InjectFrom(locationDTO) as Location;
            locationRepo.Add(newLocation);
            unitOfWork.Commit();
            return newLocation.Id;
        }

        public List<GuestDTO> GetLocations()
        {
            return locationRepo.GetAll().Select(g => new GuestDTO().InjectFrom(g) as GuestDTO).ToList();
        }

        public List<GuestDTO> GetLocationsByFee(int fee)
        {
            return locationRepo.Query().Where(g => g.RentFee < fee).Select(g => new GuestDTO().InjectFrom(g) as GuestDTO).ToList();
        }

        public List<GuestDTO> GetLocationsByName(String name)
        {
            return locationRepo.Query().Where(g => g.Name.Contains(name)).Select(g => new GuestDTO().InjectFrom(g) as GuestDTO).ToList();
        }

        public List<GuestDTO> GetLocationsBySize(int size)
        {
            return locationRepo.Query().Where(g => g.Capacity == size).Select(g => new GuestDTO().InjectFrom(g) as GuestDTO).ToList();
        }
    }
}
