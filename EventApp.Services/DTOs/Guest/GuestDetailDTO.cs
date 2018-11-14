using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.DTOs.Guest
{
    public class GuestDetailDTO : GuestNameDTO
    {
        public string Email { get; set; }
    }
}
