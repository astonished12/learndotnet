using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.StaffService.StaffDtos
{
    public class StaffFeeDTO
    {
        public string Name { get; set; }
        public decimal MaxFee { get; set; }
        public decimal MinFee { get; set; }
        public decimal AvgFee { get; set; }
    }
}
