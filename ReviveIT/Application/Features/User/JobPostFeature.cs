using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class JobPostFeature : IJobPostFeature
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobPostFeature(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JobPostResultDto> CreateJobPostAsync(JobPostDto jobPostDto, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return JobPostResultDto.FailureResult("UserId is missing from the token.");
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return JobPostResultDto.FailureResult("User does not exist.");
            }

            var category = await _context.Categories.FindAsync(jobPostDto.CategoryId);
            if (category == null)
            {
                return JobPostResultDto.FailureResult("Category not found.");
            }

            var city = await _context.Cities.FindAsync(jobPostDto.CityId);
            if (city == null)
            {
                return JobPostResultDto.FailureResult("City not found");
            }

            var jobPost = new Jobs
            {
                Status = JobStatus.Open,
                Title = jobPostDto.Title,
                Description = jobPostDto.Description,
                CategoryId = jobPostDto.CategoryId,
                cityId = jobPostDto.CityId,
                Price = jobPostDto.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.Jobs.Add(jobPost);
            await _context.SaveChangesAsync();

            var response = new JobPostResponseDto
            {
                JobID = jobPost.JobID,
                Title = jobPost.Title,
                Description = jobPost.Description,
                Status = jobPost.Status.ToString(),
                CreatedAt = jobPost.CreatedAt,
                Price = jobPost.Price,
                CategoryName = category.Name,
                CityName = city.CityName
            };

            return JobPostResultDto.SuccessResult(response, "Job post created successfully.");
        }

        public async Task<DeleteJobDto> DeleteJobPostAsync(int jobId, string userId)
        {
            var jobPost = await _context.Jobs
                .FirstOrDefaultAsync(j => j.JobID == jobId && j.UserId == userId);

            if (jobPost == null)
            {
                return DeleteJobDto.Failure("Job post not found.", 404);
            }

            var selectedApplicants = await _context.SelectedJobApplicants
                .Where(sa => sa.JobID == jobId)
                .ToListAsync();

            if (selectedApplicants.Any())
            {
                _context.SelectedJobApplicants.RemoveRange(selectedApplicants);
            }


            var jobApplications = await _context.JobApplications
                 .Where(ja => ja.JobID == jobId)
                 .ToListAsync();

            if (jobApplications.Any())
            {
                _context.JobApplications.RemoveRange(jobApplications);
            }
            _context.Jobs.Remove(jobPost);
            await _context.SaveChangesAsync();
            return DeleteJobDto.Success("Job post and associated applications deleted successfully.");
        }
    }
}