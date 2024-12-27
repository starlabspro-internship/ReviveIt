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

        public async Task<GetAllJobsResultDto> HandleAsync(GetAllJobsQuery query)
        {
            var jobsQuery = _context.Jobs
                           .Include(j => j.Category)
                           .Include(j => j.City)
                           .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Keywords))
            {
                jobsQuery = jobsQuery.Where(j => j.Title.Contains(query.Keywords) || j.Description.Contains(query.Keywords));
            }

            if (query.SelectedCityId.HasValue)
            {
                jobsQuery = jobsQuery.Where(j => j.cityId == query.SelectedCityId);
            }

            if (query.SelectedCategoryId.HasValue)
            {
                jobsQuery = jobsQuery.Where(j => j.CategoryId == query.SelectedCategoryId);
            }

            if (query.Price.HasValue)
            {
                jobsQuery = jobsQuery.Where(j => j.Price >= query.Price);
            }

            if (!string.IsNullOrEmpty(query.NumberOfApplicants))
            {
                switch (query.NumberOfApplicants)
                {
                    case "LessThan3":
                        jobsQuery = jobsQuery.Where(j => _context.JobApplications.Count(a => a.JobID == j.JobID) < 3);
                        break;
                    case "LessThan5":
                        jobsQuery = jobsQuery.Where(j => _context.JobApplications.Count(a => a.JobID == j.JobID) < 5);
                        break;
                    case "LessThan10":
                        jobsQuery = jobsQuery.Where(j => _context.JobApplications.Count(a => a.JobID == j.JobID) < 10);
                        break;
                    case "LessThan20":
                        jobsQuery = jobsQuery.Where(j => _context.JobApplications.Count(a => a.JobID == j.JobID) < 20);
                        break;
                }
            }

            jobsQuery = query.SortBy switch
            {
                "highestPrice" => jobsQuery.OrderByDescending(j => j.Price),
                "dateAsc" => jobsQuery.OrderBy(j => j.CreatedAt),
                "dateDesc" => jobsQuery.OrderByDescending(j => j.CreatedAt),
                "nameAsc" => jobsQuery.OrderBy(j => j.Title),
                "nameDesc" => jobsQuery.OrderByDescending(j => j.Title),
                _ => jobsQuery
            };

            if (jobsQuery == null || !jobsQuery.Any())
            {
                return new GetAllJobsResultDto
                {
                    Success = true,
                    Message = "No jobs posted yet.",
                    Jobs = new List<GetJobsDto>()
                };
            }

            var jobs = await jobsQuery
                     .Select(job => new
                     {
                         Job = job,
                         NumberOfApplicants = _context.JobApplications.Count(a => a.JobID == job.JobID)
                     })
                     .ToListAsync();

            var jobsDto = jobs.Select(j => new GetJobsDto
            {
                JobID = j.Job.JobID,
                Title = j.Job.Title,
                Description = j.Job.Description,
                Status = j.Job.Status.ToString(),
                CreatedAt = j.Job.CreatedAt,
                CategoryName = j.Job.Category?.Name,
                CityName = j.Job.City.CityName,
                Price = j.Job.Price,
                NumberOfApplicants = j.NumberOfApplicants
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