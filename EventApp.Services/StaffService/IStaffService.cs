using EventApp.Services.StaffService.StaffDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.StaffService
{
    public interface IStaffService
    {
        IEnumerable<StaffDTO> GetStaffsByLocation(int locationId);
        IEnumerable<StaffDTO> GetStaffsByEvent(int eventId);
        IEnumerable<StaffDTO> GetStaffsByLocationAndRole(int roleId, int locationId);
        IEnumerable<StaffDTO> GetStaffsWithNoLocation();
        int CreateStaff(StaffDTO staff);
        void AssignLocationToStaff(int staffId, int locationId);
        void DeleteStaff(int staffId);
    }
}
