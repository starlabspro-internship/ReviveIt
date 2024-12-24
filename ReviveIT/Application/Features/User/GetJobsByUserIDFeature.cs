using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                                     .Include(j => j.City)
                                      .Select(j => new {
                                          Job = j,
                                          CityName = j.City.CityName,
                                          CategoryName = j.Category.Name
                                      })
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
                JobID = job.Job.JobID,
                Title = job.Job.Title,
                Description = job.Job.Description,
                Status = job.Job.Status.ToString(),
                CreatedAt = job.Job.CreatedAt,
                CategoryName = job.CategoryName,
                CityName = job.CityName
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