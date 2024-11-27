using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetJobsByUserIDFeature
    {
        private readonly IApplicationDbContext _context;

        public GetJobsByUserIDFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetJobsByUserResultDto> HandleAsync(string userId)
        {
            var jobs = await _context.Jobs
                                     .Where(j => j.UserId == userId)
                                     .Include(j => j.Category)
                                     .ToListAsync();

            if (jobs == null || !jobs.Any())
            {
                return new GetJobsByUserResultDto
                {
                    Success = true,
                    Message = "You haven't posted any jobs yet.",
                    Jobs = new List<GetJobsDto>()
                };
            }

            var jobsDto = jobs.Select(job => new GetJobsDto
            {
                JobID = job.JobID,
                Title = job.Title,
                Description = job.Description,
                Status = job.Status.ToString(),
                CreatedAt = job.CreatedAt,
                CategoryName = job.Category?.Name
            }).ToList();

            return new GetJobsByUserResultDto
            {
                Success = true,
                Message = "Jobs retrieved successfully.",
                Jobs = jobsDto
            };
        }
    }
}