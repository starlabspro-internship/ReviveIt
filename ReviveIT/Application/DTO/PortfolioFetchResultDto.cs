namespace Application.DTO
{
    public class PortfolioFetchResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<PortfolioFetchDto> PortfolioDocuments { get; set; }
    }
}