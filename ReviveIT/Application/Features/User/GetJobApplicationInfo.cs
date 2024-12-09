using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetJobApplicationInfo
    {
        private readonly IApplicationDbContext _context;

        public GetJobApplicationInfo(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetJobApplicationInfoDto> CheckIfUserAppliedForJobAsync(int jobId, string userId)
        {
            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(ja => ja.JobID == jobId && ja.UserId == userId);

            if (jobApplication != null)
            {
                return new GetJobApplicationInfoDto
                {
                    HasApplied = true,
                    ApplicationId = jobApplication.ApplicationID
                };
            }

            return new GetJobApplicationInfoDto
            {
                HasApplied = false
            };
        }
    }
}