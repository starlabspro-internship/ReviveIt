namespace Application.DTO
{
    public class UpdateReviewResultDto
    {
        public int ReviewId { get; set; }
        public string Message { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Success { get; set; }
    }
}