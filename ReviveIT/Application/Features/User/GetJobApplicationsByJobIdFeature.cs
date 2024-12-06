using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features
{
    public class GetJobApplicationsByJobIdFeature
    {
        private readonly IApplicationDbContext _context;

        public GetJobApplicationsByJobIdFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplicationDto>> GetJobApplicationsByJobIdAsync(int jobId, string userId)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobID == jobId && j.UserId == userId);

            if (job == null)
            {
                return null; 
            }

            var applications = await _context.JobApplications
                .Where(ja => ja.JobID == jobId)
                .Select(ja => new JobApplicationDto
                {
                    ApplicationID = ja.ApplicationID,
                    ApplicantUserId = ja.UserId,
                    Status = ja.Status,
                    ApplicationDate = ja.ApplicationDate
                })
                .ToListAsync();

            return applications;
        }
    }
}