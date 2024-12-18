namespace Application.DTO
{
    public class GetAllReviewsResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ReviewDetailsDto> Reviews { get; set; }
    }
}