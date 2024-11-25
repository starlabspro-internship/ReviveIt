using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            var jobPost = new Jobs
            {
                Title = jobPostDto.Title,
                Description = jobPostDto.Description,
                CategoryId = jobPostDto.CategoryId,
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
                CategoryName = category.Name 
            };

            return JobPostResultDto.SuccessResult(response, "Job post created successfully.");
        }
    }
}
