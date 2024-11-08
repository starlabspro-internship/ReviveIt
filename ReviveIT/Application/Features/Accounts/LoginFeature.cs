using Application.Common.Exceptions;
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

        public async Task<string?> AuthenticateUser(LoginRequestDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new NotFoundException();

            if (user.EmailConfirmed == false)
                throw new UnauthorizedException("Email not verified.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedException("Password incorrect.");
            
            return _tokenHelper.GenerateToken(user);
        }
    }
}
