using System.Security.Claims;
using Application.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User
{
    public class UserInfoFeature
    {
        private readonly UserManager<Users> _userManager;

        public UserInfoFeature(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserInfoResultDto> HandleAsync(ClaimsPrincipal userClaims, string type)
        {
            var userIdClaim = userClaims.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return UserInfoResultDto.ErrorResult("User ID not found in token");

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null)
                return UserInfoResultDto.ErrorResult("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            switch (type.ToLower())
            {
                case "fullname":
                    {
                        string? displayName;

                        if (roles.Contains("Company"))
                        {
                            displayName = user.CompanyName;
                        }
                        else
                        {
                            displayName = user.FullName;
                        }
                        return UserInfoResultDto.SuccessResult(new { fullName = displayName });
                    }
                case "role":
                    return UserInfoResultDto.SuccessResult(new { role = roles.FirstOrDefault() ?? "Unknown" });
                case "experience":
                    return UserInfoResultDto.SuccessResult(new { experience = user.Experience });
                default:
                    return UserInfoResultDto.ErrorResult("Invalid type specified");
            }
        }
    }
}