using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class UpdateReviewFeature
    {
        private readonly IApplicationDbContext _context;

        public UpdateReviewFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateReviewResultDto> ExecuteAsync(int reviewId, UpdateReviewDto updateReviewDto, string userId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewID == reviewId);

            if (review == null)
            {
                return new UpdateReviewResultDto
                {
                    Success = false,
                    Message = "Review not found."
                };
            }

            if (review.UserId != userId)
            {
                return new UpdateReviewResultDto
                {
                    Success = false,
                    Message = "You can only update your own reviews."
                };
            }

            review.Content = updateReviewDto.Content;
            review.Rating = updateReviewDto.Rating;
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UpdateReviewResultDto
            {
                Success = true,
                ReviewId = review.ReviewID,
                Message = "Review successfully updated.",
                UpdatedAt = review.UpdatedAt
            };
        }
    }
}