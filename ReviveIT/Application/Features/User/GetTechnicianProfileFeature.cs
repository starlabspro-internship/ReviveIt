using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class GetTechnicianProfileFeature
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public GetTechnicianProfileFeature(IApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ProfileDataResultDto> ExecuteAsync(string id)
        {
            var user = await _context.Users
                .Include(u => u.Portfolios)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return ProfileDataResultDto.Failure("Technician not found.");

            string defaultProfilePicture = "/images/defaultProfilePicture.png";
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Company"))
            {
                defaultProfilePicture = "/images/defaultCompanyPicture.png";
            }

            string? profilePictureUrl = string.IsNullOrEmpty(user.ProfilePicture) ? defaultProfilePicture : user.ProfilePicture;
            
            var profile = new
            {
                FullName = user.FullName,
                CompanyName = user.CompanyName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ProfilePictureUrl = profilePictureUrl, 
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