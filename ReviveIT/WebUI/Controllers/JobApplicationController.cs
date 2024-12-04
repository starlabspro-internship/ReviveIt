using Application.DTO;
using Application.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase 
    {
        private readonly ApplyForJobFeature _applyForJobFeature;

        public JobApplicationController(ApplyForJobFeature applyForJobFeature)
        {
            _applyForJobFeature = applyForJobFeature;
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpPost("apply-for-job/{jobId}")]
        public async Task<IActionResult> ApplyForJob(int jobId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new CreateJobApplicationResultDTO { Success = false, Message = "User not authenticated." });

            var result = await _applyForJobFeature.ApplyForJobAsync(jobId, userIdClaim);

            if (result.Success)
                return Ok(result); 

            return BadRequest(result); 
        }
    }
}
