using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetPortfolioPhotosFeature
    {
        private readonly IApplicationDbContext _dbContext;

        public GetPortfolioPhotosFeature(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PortfolioFetchResultDto> GetPortfolioPhotosAsync(string userId)
        {
            try
            {
                var portfolioPhotos = await _dbContext.PortfolioDocuments
                    .Where(doc => doc.UserId == userId)
                    .OrderByDescending(doc => doc.UploadedAt)
                    .ToListAsync();

                if (!portfolioPhotos.Any())
                {
                    return new PortfolioFetchResultDto
                    {
                        IsSuccess = false,
                        Message = "No portfolio items found for the user.",
                        PortfolioDocuments = new List<PortfolioFetchDto>()
                    };
                }

                var portfolioDtos = portfolioPhotos.Select(photo => new PortfolioFetchDto
                {
                    Id = photo.Id,
                    Description = photo.Description, 
                    FilePath = photo.FilePath,
                    UploadedAt = photo.UploadedAt
                }).ToList();

                return new PortfolioFetchResultDto
                {
                    IsSuccess = true,
                    Message = "Portfolio retrieved successfully.",
                    PortfolioDocuments = portfolioDtos
                };
            }
            catch (Exception ex)
            {
                return new PortfolioFetchResultDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    PortfolioDocuments = null
                };
            }
        }
    }
}