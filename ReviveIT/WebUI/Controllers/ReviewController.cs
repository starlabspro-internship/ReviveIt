using Application.DTO;
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

        public ReviewController(IApplicationDbContext context, UserManager<Users> userManager, CreateReviewFeature createReviewFeature, UpdateReviewFeature updateReviewFeature)
        {
            _context = context;
            _userManager = userManager;
            _createReviewFeature = createReviewFeature;
            _updateReviewFeature = updateReviewFeature;
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
    }
}