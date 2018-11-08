using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Data.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Fee { get; set; }
    }
}
