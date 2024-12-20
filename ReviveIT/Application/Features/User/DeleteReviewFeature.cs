using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class DeleteReviewFeature
    {
        private readonly IApplicationDbContext _context;

        public DeleteReviewFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteReviewResultDto> ExecuteAsync(int reviewId, string userId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewID == reviewId);

            if (review == null)
            {
                return new DeleteReviewResultDto
                {
                    Success = false,
                    Message = "Review not found."
                };
            }

            if (review.UserId != userId)
            {
                return new DeleteReviewResultDto
                {
                    Success = false,
                    Message = "You can only delete your own reviews."
                };
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return new DeleteReviewResultDto
            {
                Success = true,
                Message = "Review successfully deleted."
            };
        }
    }
}