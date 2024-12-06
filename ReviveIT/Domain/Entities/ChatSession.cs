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

        public Users Technician { get; set; }
        public Users Customer { get; set; }
        public Users Company { get; set; }
        public ICollection<Messages> Messages { get; set; }
    }
}