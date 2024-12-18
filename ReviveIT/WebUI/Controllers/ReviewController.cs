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

        public ReviewController(IApplicationDbContext context, UserManager<Users> userManager, CreateReviewFeature createReviewFeature)
        {
            _context = context;
            _userManager = userManager;
            _createReviewFeature = createReviewFeature;
        }

        [HttpPost("users/{reviewedUserId}/reviews")]
        public async Task<IActionResult> CreateReview(string reviewedUserId, [FromBody] CreateReviewDto createReviewDto)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (reviewedUserId != createReviewDto.ReviewedUserId)
            {
                return BadRequest("Reviewed user ID mismatch.");
            }

            try
            {
                var result = await _createReviewFeature.ExecuteAsync(createReviewDto, userId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
