using Application.Features.User;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetJobsController : ControllerBase
    {
        private readonly GetAllJobsFeature _getAllJobsFeature;
        private readonly GetJobsByUserIDFeature _getJobsByUserIDFeature;

        public GetJobsController(GetAllJobsFeature getAllJobsFeature, GetJobsByUserIDFeature getJobsByUserIDFeature)
        {
            _getAllJobsFeature = getAllJobsFeature;
            _getJobsByUserIDFeature = getJobsByUserIDFeature;
        }

        [HttpGet("get-all-jobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var result = await _getAllJobsFeature.HandleAsync();
            return Ok(result);  
        }

        [HttpGet("get-jobs-by-user-id")]
        public async Task<IActionResult> GetJobsByUserID()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { success = false, message = "User not authenticated." });
            }

            var result = await _getJobsByUserIDFeature.HandleAsync(userIdClaim);
            return Ok(result);  
        }
    }
}