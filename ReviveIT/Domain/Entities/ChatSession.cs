using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChatSession
    {
        public int ChatSessionId { get; set; }
        public string TechnicianId { get; set; }
        public string CustomerId { get; set; }
        public string? CompanyId { get; set; } 
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        // Navigation properties
        public Users Technician { get; set; }
        public Users Customer { get; set; }
        public Users Company { get; set; }
        public ICollection<Messages> Messages { get; set; }
    }
}
