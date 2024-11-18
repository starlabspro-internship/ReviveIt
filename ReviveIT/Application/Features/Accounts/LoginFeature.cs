using Application.DTO;
using Application.Helpers;
using Application.Interfaces; 
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Accounts
{
    public class LoginFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly TokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository; 
        private readonly ConfigurationConstant _constant;

        public LoginFeature(UserManager<Users> userManager,
                            SignInManager<Users> signInManager,
                            TokenHelper tokenHelper,
                            IRefreshTokenRepository refreshTokenRepository, 
                            ConfigurationConstant constant)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository; 
            _constant = constant;
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

            var refreshToken = _tokenHelper.GenerateRefreshToken();

            var existingRefreshToken = await _refreshTokenRepository.GetByTokenAsync(user.Id);
            if (existingRefreshToken != null)
            {
                await _refreshTokenRepository.RemoveRefreshTokenAsync(user.Id);
            }

            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresOn = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddOrUpdateRefreshTokenAsync(userRefreshToken);

            return LoginResultDTO.Success(token);
        }
    }
}
