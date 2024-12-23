using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

            var selectedApplicant = await _context.SelectedJobApplicants
                .FirstOrDefaultAsync(sa => sa.JobID == jobId);

            var applications = await _context.JobApplications
                .Where(ja => ja.JobID == jobId)
                .ToListAsync();

            var applicationDtos = applications.Select(ja =>
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == ja.UserId);

                string applicantName = null;
                if (user != null)
                {
                    applicantName = user.FullName != null ? user.FullName : user.CompanyName;
                }
                return new JobApplicationDto
                {
                    ApplicationID = ja.ApplicationID,
                    ApplicantUserId = ja.UserId,
                    ApplicantName = applicantName,
                    Status = selectedApplicant != null && selectedApplicant.ApplicationID == ja.ApplicationID ? "Selected" : null,
                    ApplicationDate = ja.ApplicationDate
                };
            }).ToList();

            return applicationDtos;
        }
    }
}