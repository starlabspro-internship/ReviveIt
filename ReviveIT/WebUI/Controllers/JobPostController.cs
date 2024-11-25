using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobPostFeature _jobPostFeature;

        public JobController(IJobPostFeature jobPostFeature)
        {
            _jobPostFeature = jobPostFeature;
        }

        [HttpPost("create-job")]
        public async Task<IActionResult> CreateJobPost(JobPostDto jobPostDto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User not authenticated");
            }

            var result = await _jobPostFeature.CreateJobPostAsync(jobPostDto, userIdClaim);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}