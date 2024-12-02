namespace Domain.Entities
{
    public class PortfolioDocument
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string FileType { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }
    }
}