using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class CreateReviewFeature
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public CreateReviewFeature(IApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<CreateReviewResultDto> ExecuteAsync(CreateReviewDto createReviewDto, string userId)
        {
            if (userId == createReviewDto.ReviewedUserId)
            {
                return new CreateReviewResultDto
                {
                    Success = false,
                    Message = "You cannot review yourself."
                };
            }

            var reviewedUser = await _context.Users
                                             .FirstOrDefaultAsync(u => u.Id == createReviewDto.ReviewedUserId);

            if (reviewedUser == null)
            {
                return new CreateReviewResultDto
                {
                    Success = false,
                    Message = "User to be reviewed does not exist."
                };
            }

            if (reviewedUser.Role != UserRole.Technician && reviewedUser.Role != UserRole.Company)
            {
                return new CreateReviewResultDto
                {
                    Success = false,
                    Message = "You can only review technicians or companies."
                };
            }

            var reviewer = await _context.Users.FindAsync(userId);
            if (reviewer == null)
            {
                return new CreateReviewResultDto
                {
                    Success = false,
                    Message = "Reviewer user does not exist."
                };
            }

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ReviewedUserId == createReviewDto.ReviewedUserId);

            if (existingReview != null)
            {
                return new CreateReviewResultDto
                {
                    Success = false,
                    Message = "You have already reviewed this technician or company."
                };
            }

            var review = new Reviews
            {
                UserId = userId,
                ReviewedUserId = createReviewDto.ReviewedUserId,
                Content = createReviewDto.Content,
                Rating = createReviewDto.Rating,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return new CreateReviewResultDto
            {
                Success = true,
                ReviewId = review.ReviewID,
                Message = "Review successfully created.",
                CreatedAt = review.CreatedAt
            };
        }
    }
}