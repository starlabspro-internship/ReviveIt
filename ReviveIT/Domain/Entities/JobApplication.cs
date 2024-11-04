namespace Domain.Entities
{
    public class JobApplication
    {
        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }

        public int JobID { get; set; }
        public Jobs Job { get; set; }

        public string UserId { get; set; }  
        public Users User { get; set; }
    }
}
