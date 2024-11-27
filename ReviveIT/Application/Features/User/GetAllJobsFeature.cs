using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetAllJobsFeature
    {
        private readonly IApplicationDbContext _context;

        public GetAllJobsFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllJobsResultDto> HandleAsync()
        {
            var jobs = await _context.Jobs
                                     .Include(j => j.Category)
                                     .ToListAsync();

            if (jobs == null || !jobs.Any())
            {
                return new GetAllJobsResultDto
                {
                    Success = true,
                    Message = "No jobs posted yet.",
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

            return new GetAllJobsResultDto
            {
                Success = true,
                Message = "Jobs retrieved successfully.",
                Jobs = jobsDto
            };
        }
    }
}