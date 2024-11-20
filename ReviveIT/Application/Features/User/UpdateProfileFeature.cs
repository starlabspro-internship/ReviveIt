using Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User
{
    public class UpdateProfileFeature
    {
        private readonly UserManager<Users> _userManager;

        public UpdateProfileFeature(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UpdateProfileAsync(string userId, UpdateProfileDTO updateProfileDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            switch (userRole)
            {
                case nameof(UserRole.Admin):
                case nameof(UserRole.Customer):
                    user.FullName = updateProfileDTO.FullName ?? user.FullName;
                    break;

                case nameof(UserRole.Technician):
                    user.FullName = updateProfileDTO.FullName ?? user.FullName;
                    user.Expertise = updateProfileDTO.Expertise ?? user.Expertise;
                    user.Experience = updateProfileDTO.Experience ?? user.Experience;
                    break;

                case nameof(UserRole.Company):
                    user.FullName = updateProfileDTO.FullName ?? user.FullName;
                    user.CompanyName = updateProfileDTO.CompanyName ?? user.CompanyName;
                    user.CompanyAddress = updateProfileDTO.CompanyAddress ?? user.CompanyAddress;
                    break;

                default:
                    return false; 
            }

            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
