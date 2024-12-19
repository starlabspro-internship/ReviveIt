namespace Application.DTO
{
    public class GetAverageRatingResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}