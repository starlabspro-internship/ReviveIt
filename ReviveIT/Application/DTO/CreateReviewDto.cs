namespace Application.DTO
{
    public class CreateReviewDto
    {
        public string ReviewedUserId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}