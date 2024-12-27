namespace Application.DTO
{
    public class JobApplicationDto
    {
        public int ApplicationID { get; set; }
        public string ApplicantUserId { get; set; }
        public string? ApplicantName { get; set; }
        public string Status { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}