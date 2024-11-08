using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Application.Helpers;
using System.Threading.Tasks;
using System;
using System.Linq;
using Application.DTO;

namespace Application.Features.Accounts
{
    public class RegisterFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly TokenHelper _tokenHelper;

        public RegisterFeature(UserManager<Users> userManager, TokenHelper tokenHelper)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
        }

        public async Task<RegisterResultDto> RegisterUserAsync(RegisterDto dto)
        {
            if (!ValidationHelper.ValidateRegisterDto(dto, out var validationMessage))
            {
                return new RegisterResultDto { Success = false, Message = validationMessage };
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new RegisterResultDto { Success = false, Message = "An account with this email already exists." };
            }

            var user = CreateUserFromDto(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResultDto { Success = false, Message = errorMessage };
            }

            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
            {
                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return new RegisterResultDto { Success = false, Message = $"Failed to assign role: {roleErrors}" };
            }

            var token = _tokenHelper.GenerateToken(user);
            return new RegisterResultDto { Success = true, Token = token, Message = "Registration successful!" };
        }

        private Users CreateUserFromDto(RegisterDto dto) => new Users
        {
            UserName = dto.Email,
            Email = dto.Email,
            Role = dto.Role,
            FullName = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Expertise = dto.Expertise,
            Experience = dto.Experience,
            CompanyName = dto.CompanyName,
            CompanyAddress = dto.CompanyAddress
        };
    }
}
