using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public class AddPortfolioPhotoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}