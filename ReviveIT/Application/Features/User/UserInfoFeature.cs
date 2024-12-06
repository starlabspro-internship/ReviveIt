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

            return type.ToLower() switch
            {
                "fullname" => UserInfoResultDto.SuccessResult(new { FullName = user.FullName }),
                "role" => UserInfoResultDto.SuccessResult(new { Role = user.Role.ToString() }),
                _ => UserInfoResultDto.ErrorResult("Invalid type specified")
            };
        }
    }
}