using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class DeleteJobApplicationFeature
    {
        private readonly IApplicationDbContext _context;

        public DeleteJobApplicationFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteJobApplicationResultDTO> DeleteJobApplicationAsync(int applicationId, string userId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.Job)
                .FirstOrDefaultAsync(ja => ja.ApplicationID == applicationId);

            if (jobApplication == null)
            {
                return new DeleteJobApplicationResultDTO
                {
                    Success = false,
                    Message = "Job application not found."
                };
            }

            if (jobApplication.UserId != userId)
            {
                return new DeleteJobApplicationResultDTO
                {
                    Success = false,
                    Message = "You are not authorized to delete this job application."
                };
            }

            var selectedApplicant = await _context.SelectedJobApplicants
                .FirstOrDefaultAsync(sja => sja.ApplicationID == applicationId);

            if (selectedApplicant != null)
            {
                _context.SelectedJobApplicants.Remove(selectedApplicant);

                var job = jobApplication.Job;
                job.Status = JobStatus.Open;

                _context.JobApplications.Remove(jobApplication);

                await _context.SaveChangesAsync();

                return new DeleteJobApplicationResultDTO
                {
                    Success = true,
                    Message = "Job application deleted successfully and job status reverted to Open."
                };
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();

            return new DeleteJobApplicationResultDTO
            {
                Success = true,
                Message = "Job application deleted successfully."
            };
        }
    }
}