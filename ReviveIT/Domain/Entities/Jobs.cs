namespace Domain.Entities
{
    public class Jobs
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
        public string UserId { get; set; }
        public Users User { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
