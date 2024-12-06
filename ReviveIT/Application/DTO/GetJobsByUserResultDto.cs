namespace Application.DTO
{
    public class GetJobsByUserResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<GetJobsDto> Jobs { get; set; }
    }
}