namespace Application.DTO
{
    public class GetJobsDto
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public decimal Price { get; set; }
        public int NumberOfApplicants { get; set; }
    }
}