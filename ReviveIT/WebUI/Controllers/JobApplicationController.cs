using Application.DTO;
using Application.Features;
using Application.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Application.Interfaces;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly ApplyForJobFeature _applyForJobFeature;
        private readonly DeleteJobApplicationFeature _deleteJobApplicationFeature;
        private readonly SelectJobApplicantFeature _selectJobApplicantFeature;
        private readonly GetJobApplicationsByJobIdFeature _getJobApplicationsByJobIdFeature;
        private readonly IApplicationDbContext _context;
        private readonly GetJobApplicationInfo _getJobApplicationInfo;

        public JobApplicationController(ApplyForJobFeature applyForJobFeature,
            DeleteJobApplicationFeature deleteJobApplicationFeature,
            SelectJobApplicantFeature selectJobApplicantFeature,
            GetJobApplicationsByJobIdFeature getJobApplicationsByJobIdFeature,
            IApplicationDbContext context,
            GetJobApplicationInfo getJobApplicationInfo) 
        {
            _applyForJobFeature = applyForJobFeature;
            _deleteJobApplicationFeature = deleteJobApplicationFeature;
            _selectJobApplicantFeature = selectJobApplicantFeature;
            _getJobApplicationsByJobIdFeature = getJobApplicationsByJobIdFeature;
            _context = context;
            _getJobApplicationInfo = getJobApplicationInfo;
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

        [Authorize]
        [HttpPost("select-job-applicant/{applicationId}")]
        public async Task<IActionResult> SelectJobApplicant(int applicationId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _selectJobApplicantFeature.SelectApplicantAsync(applicationId, userIdClaim);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("get-job-applications/{jobId}")]
        public async Task<IActionResult> GetJobApplicationsByJobId(int jobId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { Message = "User not authenticated." });

            var applications = await _getJobApplicationsByJobIdFeature.GetJobApplicationsByJobIdAsync(jobId, userIdClaim);

            if (applications == null)
                return NotFound(new { Message = "No applications found or you are not the creator of this job." });

            return Ok(applications);
        }

        [HttpGet("has-applied/{jobId}")]
        public async Task<IActionResult> HasUserAppliedForJob(int jobId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();  
            }

            var result = await _getJobApplicationInfo.CheckIfUserAppliedForJobAsync(jobId, userIdClaim);

            return Ok(result);
        }
    }
}