namespace Domain.Entities
{
    public class Jobs
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JobStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }

        public int CategoryId { get; set; }  
        public Category Category { get; set; }

        public decimal Price { get; set; } 

        public int cityId { get; set; }
        public City City { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; }
        public ICollection<SelectedJobApplicant> SelectedJobApplicants { get; set; }
    }

    public enum JobStatus
    {
        Open=1,
        Closed=2,
        InProgress=3,
        Completed=4
    }
}