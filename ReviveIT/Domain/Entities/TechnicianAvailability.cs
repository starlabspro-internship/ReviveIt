using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TechnicianAvailability
    {
        public int AvailabilityID { get; set; }
        public string TechnicianId { get; set; }
        public Users Technician { get; set; }
        public string DaysAvailable { get; set; }
        public string MonthsUnavailable { get; set; }
        public string SpecificUnavailableDates { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
