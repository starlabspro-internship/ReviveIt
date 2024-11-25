using Application.DTO;

namespace Application.Interfaces
{
    public interface IJobPostFeature
    {
        Task<JobPostResultDto> CreateJobPostAsync(JobPostDto jobPostDto, string userId);
    }
}