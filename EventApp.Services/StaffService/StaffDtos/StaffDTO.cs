using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.StaffService.StaffDtos
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Fee { get; set; }
        public int StaffRoleId { get; set; }
    }
}
