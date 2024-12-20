using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Application.DTO;
using Domain.Entities;

namespace Application.Features.User
{
    public class ProfilePictureFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly IHostEnvironment _hostEnvironment;

        public ProfilePictureFeature(UserManager<Users> userManager, IHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ProfilePictureResultDTO> UploadProfilePictureAsync(IFormFile profilePicture, string userIdClaim)
        {
            if (profilePicture == null || profilePicture.Length == 0)
                return ProfilePictureResultDTO.Failure("No file uploaded.");

            if (string.IsNullOrEmpty(userIdClaim))
                return ProfilePictureResultDTO.Failure("User not found.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return ProfilePictureResultDTO.Failure("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return ProfilePictureResultDTO.Failure("User not found.");

            var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/images/profile");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(profilePicture.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(fileStream);
            }

            user.ProfilePicture = $"/images/profile/{fileName}";
            await _userManager.UpdateAsync(user);

            return ProfilePictureResultDTO.Success(user.ProfilePicture);
        }

        public async Task<ProfilePictureResultDTO> RemoveProfilePictureAsync(string userIdClaim)
        {
            if (string.IsNullOrEmpty(userIdClaim))
                return ProfilePictureResultDTO.Failure("User not found.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return ProfilePictureResultDTO.Failure("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return ProfilePictureResultDTO.Failure("User not found.");

            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {

                var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", user.ProfilePicture.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            user.ProfilePicture = null;
            await _userManager.UpdateAsync(user);

            var defaultProfilePicture = "/images/defaultProfilePicture.png";

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Company"))
            {
                defaultProfilePicture = "/images/defaultCompanyPicture.png";

            }

            return ProfilePictureResultDTO.Success(defaultProfilePicture);
        }

        public async Task<ProfilePictureResultDTO> GetProfilePictureAsync(string userIdClaim)
        {
            if (string.IsNullOrEmpty(userIdClaim))
                return ProfilePictureResultDTO.Failure("User not found.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return ProfilePictureResultDTO.Failure("Invalid User ID.");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return ProfilePictureResultDTO.Failure("User not found.");

            var defaultProfilePicture = "/images/defaultProfilePicture.png";
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Company"))
            {
                defaultProfilePicture = "/images/defaultCompanyPicture.png";

            }

            var profilePictureUrl = string.IsNullOrEmpty(user.ProfilePicture) ? defaultProfilePicture : user.ProfilePicture;

            return ProfilePictureResultDTO.Success(profilePictureUrl);
        }
    }
}