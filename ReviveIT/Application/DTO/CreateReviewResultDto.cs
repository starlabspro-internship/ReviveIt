namespace Application.DTO
{
    public class CreateReviewResultDto
    {
        public int ReviewId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Success { get; set; }
    }
}