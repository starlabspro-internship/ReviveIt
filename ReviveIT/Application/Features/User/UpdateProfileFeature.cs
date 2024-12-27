using Application.DTO;
using Domain.Entities;
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

        public async Task<UpdateProfileResultDTO> UpdateProfileAsync(string userId, UpdateProfileDTO updateProfileDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new UpdateProfileResultDTO
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            try
            {
                switch (userRole)
                {
                    case nameof(UserRole.Admin):
                    case nameof(UserRole.Customer):
                        user.FullName = updateProfileDTO.FullName ?? user.FullName;
                        user.PhoneNumber = updateProfileDTO.PhoneNumber ?? user.PhoneNumber;
                        break;

                    case nameof(UserRole.Technician):
                        user.FullName = updateProfileDTO.FullName ?? user.FullName;
                        user.Expertise = updateProfileDTO.Expertise ?? user.Expertise;
                        user.Experience = updateProfileDTO.Experience ?? user.Experience;
                        user.PhoneNumber = updateProfileDTO.PhoneNumber ?? user.PhoneNumber;
                        user.Description = updateProfileDTO.Description ?? user.Description;
                        break;

                    case nameof(UserRole.Company):
                        user.FullName = updateProfileDTO.FullName ?? user.FullName;
                        user.CompanyName = updateProfileDTO.CompanyName ?? user.CompanyName;
                        user.CompanyAddress = updateProfileDTO.CompanyAddress ?? user.CompanyAddress;
                        user.PhoneNumber = updateProfileDTO.PhoneNumber ?? user.PhoneNumber;
                        user.Description = updateProfileDTO.Description ?? user.Description;
                        break;

                    default:
                        return new UpdateProfileResultDTO
                        {
                            IsSuccess = false,
                            Message = "Invalid user role."
                        };
                }

                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new UpdateProfileResultDTO
                    {
                        IsSuccess = false,
                        Message = "Failed to update user.",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new UpdateProfileResultDTO
                {
                    IsSuccess = true,
                    Message = "Profile updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new UpdateProfileResultDTO
                {
                    IsSuccess = false,
                    Message = "An error occurred while updating the profile.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}