using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TechnicianAvailabilityDto
    {
        public string DaysAvailable { get; set; }
        public string MonthsUnavailable { get; set; }
        public string SpecificUnavailableDates { get; set; }
    }
}