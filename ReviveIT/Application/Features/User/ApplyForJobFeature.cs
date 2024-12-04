using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class ApplyForJobFeature
    {
        private readonly IApplicationDbContext _context;

        public ApplyForJobFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateJobApplicationResultDTO> ApplyForJobAsync(int jobId, string userId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return new CreateJobApplicationResultDTO
                {
                    Success = false,
                    Message = "Job not found."
                };
            }

            var existingApplication = await _context.JobApplications
                .FirstOrDefaultAsync(ja => ja.JobID == jobId && ja.UserId == userId);

            if (existingApplication != null)
            {
                return new CreateJobApplicationResultDTO
                {
                    Success = false,
                    Message = "You have already applied for this job."
                };
            }

            var jobApplication = new JobApplication
            {
                JobID = jobId,
                UserId = userId,
                ApplicationDate = DateTime.UtcNow,
                Status = "Applied"
            };

            if (job.Status != JobStatus.Open)
            {
                return new CreateJobApplicationResultDTO
                {
                    Success = false,
                    Message = "This job is no longer accepting applications."
                };
            }

            if (job.UserId == userId)
            {
                return new CreateJobApplicationResultDTO
                {
                    Success = false,
                    Message = "You cannot apply to your own job."
                };
            }

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return new CreateJobApplicationResultDTO
            {
                Success = true,
                Message = "Job application submitted successfully.",
                ApplicationID = jobApplication.ApplicationID
            };
        }
    }
}