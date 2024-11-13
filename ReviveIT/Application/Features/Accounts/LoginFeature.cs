using Application.DTO;
using Application.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Accounts
{
    public class LoginFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly TokenHelper _tokenHelper;

        public LoginFeature(UserManager<Users> userManager, SignInManager<Users> signInManager, TokenHelper tokenHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
        }

        public async Task<LoginResultDTO> AuthenticateUser(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return LoginResultDTO.Failure("User not found.");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return LoginResultDTO.Failure("Password incorrect.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenHelper.GenerateToken(user);

            // Determine the redirect URL based on the user's role
            string redirectUrl = roles.Contains("Company") ? "/Company/Inbox"
                               : roles.Contains("Technician") ? "/Technician/Inbox"
                               : roles.Contains("Customer") ? "/Customer/Inbox"
                               : "/";

            return LoginResultDTO.Success(token, redirectUrl);
        }
    }
}
