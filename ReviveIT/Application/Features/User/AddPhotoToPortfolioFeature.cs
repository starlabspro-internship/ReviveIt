using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class AddPhotoToPortfolioFeature
    {
        private readonly IApplicationDbContext _dbContext;

        public AddPhotoToPortfolioFeature(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PortfolioUploadResultDto> AddPhotoAsync(AddPortfolioPhotoDto dto, string userId)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return new PortfolioUploadResultDto
                    {
                        IsSuccess = false,
                        Message = "User not found."
                    };
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(dto.Photo.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new PortfolioUploadResultDto
                    {
                        IsSuccess = false,
                        Message = "Invalid file type. Only image files are allowed."
                    };
                }

                var uploadsFolder = Path.Combine("wwwroot", "images", "Portfolio", userId);
                Directory.CreateDirectory(uploadsFolder); 

                var uniqueFileName = $"{Guid.NewGuid()}_{dto.Photo.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }

                var portfolioDocument = new PortfolioDocument
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    FilePath = $"/images/Portfolio/{userId}/{uniqueFileName}",
                    FileType = fileExtension,
                    UserId = userId
                };

                _dbContext.PortfolioDocuments.Add(portfolioDocument);
                await _dbContext.SaveChangesAsync();

                return new PortfolioUploadResultDto
                {
                    IsSuccess = true,
                    Message = "Photo uploaded successfully.",
                    PortfolioDocumentId = portfolioDocument.Id,
                    FilePath = portfolioDocument.FilePath,
                    Title = portfolioDocument.Title
                };
            }
            catch (Exception ex)
            {
                return new PortfolioUploadResultDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}