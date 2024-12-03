namespace Application.DTO
{
    public class PortfolioUploadResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? PortfolioDocumentId { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
    }
}