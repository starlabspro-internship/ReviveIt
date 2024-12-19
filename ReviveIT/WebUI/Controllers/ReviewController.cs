using Application.DTO;
using Application.Features.Review;
using Application.Features.User;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly CreateReviewFeature _createReviewFeature;
        private readonly UpdateReviewFeature _updateReviewFeature;
        private readonly DeleteReviewFeature _deleteReviewFeature;
        private readonly GetUserReviewFeature _getUserReviewFeature;
        private readonly GetAllReviewsFeature _getAllReviewsFeature;
        private readonly GetAverageRatingFeature _getAverageRatingFeature;

        public ReviewController(IApplicationDbContext context, UserManager<Users> userManager, CreateReviewFeature createReviewFeature, UpdateReviewFeature updateReviewFeature, DeleteReviewFeature deleteReviewFeature, GetUserReviewFeature getUserReviewFeature, GetAllReviewsFeature getAllReviewsFeature, GetAverageRatingFeature getAverageRatingFeature)
        {
            _context = context;
            _userManager = userManager;
            _createReviewFeature = createReviewFeature;
            _updateReviewFeature = updateReviewFeature;
            _deleteReviewFeature = deleteReviewFeature;
            _getUserReviewFeature = getUserReviewFeature;
            _getAllReviewsFeature = getAllReviewsFeature;
            _getAverageRatingFeature = getAverageRatingFeature;
        }

        [HttpPost("users/{reviewedUserId}/reviews")]
        public async Task<IActionResult> CreateReview(string reviewedUserId, [FromBody] CreateReviewDto createReviewDto)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (reviewedUserId != createReviewDto.ReviewedUserId)
            {
                return BadRequest("Reviewed user ID mismatch.");
            }

            var result = await _createReviewFeature.ExecuteAsync(createReviewDto, userId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result); 
        }

        [HttpPut("reviews/{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] UpdateReviewDto updateReviewDto)
        {
            var userId = User.FindFirst("UserId")?.Value;

            var result = await _updateReviewFeature.ExecuteAsync(reviewId, updateReviewDto, userId);

            if (!result.Success)
            {
                return BadRequest(result.Message); 
            }

            return Ok(result); 
        }

        [HttpDelete("reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var userId = User.FindFirst("UserId")?.Value;

            var result = await _deleteReviewFeature.ExecuteAsync(reviewId, userId);

            if (!result.Success)
            {
                return BadRequest(result.Message); 
            }

            return Ok(result); 
        }

        [HttpGet("technicians/{reviewedUserId}/reviews/user")]
        public async Task<IActionResult> GetUserReviewForTechnician(string reviewedUserId)
        {
            var userId = User.FindFirst("UserId")?.Value;

            var result = await _getUserReviewFeature.ExecuteAsync(reviewedUserId, userId);

            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }

            if (result.Review == null)
            {
                return Ok(new { Message = result.Message });
            }

            return Ok(result);
        }

        [HttpGet("technicians/{reviewedUserId}/reviews")]
        public async Task<IActionResult> GetAllReviewsForTechnician(string reviewedUserId)
        {
            var result = await _getAllReviewsFeature.ExecuteAsync(reviewedUserId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("technicians/{reviewedUserId}/reviews/average-rating")]
        public async Task<IActionResult> GetAverageRatingForTechnician(string reviewedUserId)
        {
            var result = await _getAverageRatingFeature.ExecuteAsync(reviewedUserId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}