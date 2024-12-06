using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features
{
    public class SelectJobApplicantFeature
    {
        private readonly IApplicationDbContext _context;

        public SelectJobApplicantFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectApplicantResultDTO> SelectApplicantAsync(int applicationId, string userId)
        {
            var jobApplication = await _context.JobApplications
                .Include(ja => ja.Job)
                .FirstOrDefaultAsync(ja => ja.ApplicationID == applicationId);

            if (jobApplication == null)
            {
                return new SelectApplicantResultDTO
                {
                    Success = false,
                    Message = "Job application not found."
                };
            }

            if (jobApplication.Job.UserId != userId)
            {
                return new SelectApplicantResultDTO
                {
                    Success = false,
                    Message = "You are not authorized to select applicants for this job."
                };
            }

            if (jobApplication.Job.Status != JobStatus.Open && jobApplication.Job.Status != JobStatus.InProgress)
            {
                return new SelectApplicantResultDTO
                {
                    Success = false,
                    Message = "Cannot select applicants for jobs that are not open or in progress."
                };
            }

            var existingSelection = await _context.SelectedJobApplicants
                .FirstOrDefaultAsync(sja => sja.JobID == jobApplication.JobID);

            if (existingSelection != null)
            {
                existingSelection.ApplicationID = applicationId;
                existingSelection.SelectedApplicantUserId = jobApplication.UserId;
                existingSelection.SelectedDate = DateTime.UtcNow;

                _context.SelectedJobApplicants.Update(existingSelection);
            }
            else
            {
                var selectedApplicant = new SelectedJobApplicant
                {
                    ApplicationID = applicationId,
                    JobID = jobApplication.JobID,
                    SelectedApplicantUserId = jobApplication.UserId,
                    SelectedByUserId = userId,
                    SelectedDate = DateTime.UtcNow
                };

                _context.SelectedJobApplicants.Add(selectedApplicant);
            }

            jobApplication.Job.Status = JobStatus.InProgress;
            _context.Jobs.Update(jobApplication.Job);

            await _context.SaveChangesAsync();

            return new SelectApplicantResultDTO
            {
                Success = true,
                Message = "Applicant selection updated successfully.",
                SelectedApplicantID = applicationId,
                SelectedApplicantUserId = jobApplication.UserId
            };
        }
    }
}