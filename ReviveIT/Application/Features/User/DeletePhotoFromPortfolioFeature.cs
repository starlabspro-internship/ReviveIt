using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class DeletePhotoFromPortfolioFeature
    {
        private readonly IApplicationDbContext _dbContext;

        public DeletePhotoFromPortfolioFeature(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PortfolioDeleteResultDto> DeletePhotoAsync(int portfolioDocumentId, string userId)
        {
            try
            {
                var portfolioDocument = await _dbContext.PortfolioDocuments
                    .FirstOrDefaultAsync(pd => pd.Id == portfolioDocumentId && pd.UserId == userId);

                if (portfolioDocument == null)
                {
                    return new PortfolioDeleteResultDto
                    {
                        IsSuccess = false,
                        Message = "Photo not found or user does not own this photo."
                    };
                }

                var uploadsFolder = Path.Combine("wwwroot", "images", "Portfolio", userId);
                var filePath = Path.Combine(uploadsFolder, Path.GetFileName(portfolioDocument.FilePath.TrimStart('/')));

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _dbContext.PortfolioDocuments.Remove(portfolioDocument);
                await _dbContext.SaveChangesAsync();

                return new PortfolioDeleteResultDto
                {
                    IsSuccess = true,
                    Message = "Photo deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new PortfolioDeleteResultDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}