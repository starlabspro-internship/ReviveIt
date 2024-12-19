using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetUserReviewFeature
    {
        private readonly IApplicationDbContext _context;

        public GetUserReviewFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserReviewResultDto> ExecuteAsync(string reviewedUserId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new GetUserReviewResultDto
                {
                    Success = false,
                    Message = "User not logged in."
                };
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ReviewedUserId == reviewedUserId);

            if (review == null)
            {
                return new GetUserReviewResultDto
                {
                    Success = true,
                    Message = "You haven't reviewed this technician yet.",
                    Review = null
                };
            }

            return new GetUserReviewResultDto
            {
                Success = true,
                Message = "Review retrieved successfully.",
                Review = new GetUserReviewDto
                {
                    ReviewId = review.ReviewID,
                    Content = review.Content,
                    Rating = review.Rating,
                    CreatedAt = review.CreatedAt
                }
            };
        }
    }
}