using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Review
{
    public class GetAllReviewsFeature
    {
        private readonly IApplicationDbContext _context;

        public GetAllReviewsFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllReviewsResultDto> ExecuteAsync(string reviewedUserId)
        {
            var reviews = await _context.Reviews
               .Where(r => r.ReviewedUserId == reviewedUserId)
               .OrderByDescending(r => r.CreatedAt)
               .Select(r => new ReviewDetailsDto
               {
                   ReviewId = r.ReviewID,
                   Content = r.Content,
                   Rating = r.Rating,
                   CreatedAt = r.CreatedAt,
                   ReviewerName = _context.Users.FirstOrDefault(u => u.Id == r.UserId).FullName
               })
           .ToListAsync();
            if (!reviews.Any())
            {
                return new GetAllReviewsResultDto
                {
                    Success = true,
                    Message = "No reviews found for this technician.",
                    Reviews = new List<ReviewDetailsDto>()
                };
            }

            return new GetAllReviewsResultDto
            {
                Success = true,
                Message = "Reviews retrieved successfully.",
                Reviews = reviews
            };
        }
    }
}