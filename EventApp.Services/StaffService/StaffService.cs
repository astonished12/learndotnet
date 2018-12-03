using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.StaffService.StaffDtos;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace EventApp.Services.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork unitOfWork;
        private IRepository<Staff> staffRepo;
        private IRepository<Location> locationRepo;
        private IRepository<Event> eventRepo;

        public StaffService(IRepository<Staff> staffRepo, IRepository<Event> eventRepo, IRepository<Location> locationRepo, IUnitOfWork unitOfWork)
        {
            this.staffRepo = staffRepo;
            this.eventRepo = eventRepo;
            this.locationRepo = locationRepo;
            this.unitOfWork = unitOfWork;
        }

        public void AssignLocationToStaff(int staffId, int locationId)
        {

        }

        public int CreateStaff(StaffDTO staff)
        {
            Staff newStaff = new Staff().InjectFrom(staff) as Staff;
            staffRepo.Add(newStaff);
            unitOfWork.Commit();
            return newStaff.Id;
        }

        public void DeleteStaff(int staffId)
        {
            Staff staff = staffRepo.GetById(staffId);
            if (staff != null)
            {
                staffRepo.Delete(staff);
                unitOfWork.Commit();
            }
        }

        public IEnumerable<StaffDTO> GetStaffsByEvent(int eventId)
        {
            var staffs = eventRepo.Query().Include(l => l.Location)
                                        .Include(s => s.Location.Staffs)
                                        .Where(e => e.Id == eventId)
                                        .Select(s => s.Location.Staffs)
                                        .FirstOrDefault()
                                        .OrderBy(s => s.FirstName);

            return staffs.Select(s => new StaffDTO().InjectFrom(s) as StaffDTO);
        }

        public IEnumerable<StaffDTO> GetStaffsByLocation(int locationId)
        {
            return staffRepo.Query().Where(x => x.LocationId == locationId).Select(s => new StaffDTO().InjectFrom(s) as StaffDTO);
        }

        public IEnumerable<StaffDTO> GetStaffsByLocationAndRole(int roleId, int locationId)
        {
            return staffRepo.Query().Where(x => x.LocationId == locationId && x.StaffRoleId == roleId).Select(s => new StaffDTO().InjectFrom(s) as StaffDTO);
        }

        public IEnumerable<StaffDTO> GetStaffsWithNoLocation()
        {
            return staffRepo.Query().Include(x => x.Location).Where(x => x.Location == null).Select(s => new StaffDTO().InjectFrom(s) as StaffDTO);
        }
    }
}
