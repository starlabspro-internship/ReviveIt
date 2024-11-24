using Application.DTO;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Features.Accounts
{
    public class LoginFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly TokenHelper _tokenHelper;

        public LoginFeature(UserManager<Users> userManager,
                            SignInManager<Users> signInManager,
                            TokenHelper tokenHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
        }

        public async Task<LoginResultDTO> AuthenticateUser(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return LoginResultDTO.Failure("Invalid credentials.");
            }
            if (!user.EmailConfirmed)
            {
                return LoginResultDTO.Failure("Email not confirmed!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return LoginResultDTO.Failure("Invalid credentials.");
            }

            var token = _tokenHelper.GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return LoginResultDTO.Success(token, role); 
        }
    }
}
