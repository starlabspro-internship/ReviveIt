using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class DeleteJobApplicationFeature
    {
        private readonly IApplicationDbContext _context;

        public DeleteJobApplicationFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteJobApplicationResultDTO> DeleteJobApplicationAsync(int applicationId, string userId)
        {
            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(ja => ja.ApplicationID == applicationId && ja.UserId == userId);

            if (jobApplication == null)
            {
                return new DeleteJobApplicationResultDTO
                {
                    Success = false,
                    Message = "Job application not found or does not belong to you."
                };
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();

            return new DeleteJobApplicationResultDTO
            {
                Success = true,
                Message = "Job application deleted successfully."
            };
        }
    }
}