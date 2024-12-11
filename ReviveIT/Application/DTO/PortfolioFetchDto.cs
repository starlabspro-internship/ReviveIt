namespace Application.DTO
{
    public class PortfolioFetchDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}