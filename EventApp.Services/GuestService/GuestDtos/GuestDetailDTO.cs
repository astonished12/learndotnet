using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.GuestService.GuestDtos
{
    public class GuestDetailDTO : GuestNameDTO
    {
        public string Email { get; set; }
    }
}
