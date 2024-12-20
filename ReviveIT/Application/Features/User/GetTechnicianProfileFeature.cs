using Application.DTO;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetTechnicianProfileFeature
    {
        private readonly IApplicationDbContext _context;

        public GetTechnicianProfileFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileDataResultDto> ExecuteAsync(string id)
        {
            var user = await _context.Users
                .Include(u => u.Portfolios)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return ProfileDataResultDto.Failure("Technician not found.");

            var profile = new
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                Expertise = user.Expertise,
                Experience = user.Experience,
                Description = user.Description,
                Portfolios = user.Portfolios.Select(p => new
                {
                    p.Title,
                    p.Description,
                    p.FilePath,
                    p.FileType
                })
            };

            return ProfileDataResultDto.Success(profile);
        }
    }
}