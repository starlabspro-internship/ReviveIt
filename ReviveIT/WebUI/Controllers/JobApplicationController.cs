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
        private readonly DeleteJobApplicationFeature _deleteJobApplicationFeature;

        public JobApplicationController(ApplyForJobFeature applyForJobFeature, DeleteJobApplicationFeature deleteJobApplicationFeature)
        {
            _applyForJobFeature = applyForJobFeature;
            _deleteJobApplicationFeature = deleteJobApplicationFeature;
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

        [Authorize(Roles = "Technician,Company")]
        [HttpDelete("delete-job-application/{applicationId}")]
        public async Task<IActionResult> DeleteJobApplication(int applicationId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _deleteJobApplicationFeature.DeleteJobApplicationAsync(applicationId, userIdClaim);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}