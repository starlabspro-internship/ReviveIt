using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetAverageRatingFeature
    {
        private readonly IApplicationDbContext _context;

        public GetAverageRatingFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAverageRatingResultDto> ExecuteAsync(string reviewedUserId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ReviewedUserId == reviewedUserId)
                .ToListAsync();

            if (!reviews.Any())
            {
                return new GetAverageRatingResultDto
                {
                    Success = true,
                    Message = "No reviews available for this technician.",
                    AverageRating = 0,
                    TotalReviews = 0
                };
            }

            var averageRating = reviews.Average(r => r.Rating);

            return new GetAverageRatingResultDto
            {
                Success = true,
                Message = "Average rating calculated successfully.",
                AverageRating = Math.Round(averageRating, 2),
                TotalReviews = reviews.Count
            };
        }
    }
}