using Application.DTO;

namespace Application.Interfaces
{
    public interface IJobPostFeature
    {
        Task<JobPostResultDto> CreateJobPostAsync(JobPostDto jobPostDto, string userId);
        Task<DeleteJobDto> DeleteJobPostAsync(int jobId, string userId);
    }
}