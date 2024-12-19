namespace Application.DTO
{
    public class GetUserReviewResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GetUserReviewDto Review { get; set; }
    }
}