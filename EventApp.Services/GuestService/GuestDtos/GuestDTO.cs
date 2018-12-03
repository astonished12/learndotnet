using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.GuestService.GuestDtos
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

    }
}
