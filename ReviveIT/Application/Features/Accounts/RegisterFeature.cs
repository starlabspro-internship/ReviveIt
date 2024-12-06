using Application.DTO;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Features.Accounts
{
    public class RegisterFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly TokenHelper _tokenHelper;
        private readonly IEmailSender _emailSender;
        private readonly IApplicationDbContext _context;

        public RegisterFeature(UserManager<Users> userManager, TokenHelper tokenHelper, IEmailSender emailSender, IApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _emailSender = emailSender;
            _context = context;
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

            if (!Enum.IsDefined(typeof(UserRole), dto.Role) || dto.Role == (int)UserRole.Admin)
            {
                return new RegisterResultDto { Success = false, Message = "Invalid role provided." };
            }

            var user = CreateUserFromDto(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResultDto { Success = false, Message = errorMessage };
            }

            var userRole = (UserRole)dto.Role;
            var roleResult = await _userManager.AddToRoleAsync(user, userRole.ToString());
            if (!roleResult.Succeeded)
            {
                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return new RegisterResultDto { Success = false, Message = $"Failed to assign role: {roleErrors}" };
            }

            await _userManager.AddClaimsAsync(user, new[]
           {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("UserId", user.Id),
                new Claim("Email", user.Email),
                new Claim("FullName", user.FullName ?? ""),
                new Claim("CreatedAt", user.CreatedAt.ToString("O")),
                new Claim("CompanyName", user.CompanyName ?? ""),
                new Claim("CompanyAddress", user.CompanyAddress ?? "")
            });

            await SaveUserCategoriesAsync(user.Id, dto.SelectedCategoryIds);

            var token = _tokenHelper.GenerateToken(user);
            await SendEmailConfirmationAsync(user);
            return new RegisterResultDto { Success = true, Token = token, Message = "Registration successful! Please check your email to confirm your account." };
        }

        private Users CreateUserFromDto(RegisterDto dto) => new Users
        {
            UserName = dto.Email,
            Email = dto.Email,
            Role = dto.Role,
            FullName = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Experience = dto.Experience,
            CompanyName = dto.CompanyName,
            CompanyAddress = dto.CompanyAddress,
            Expertise = string.Join(",", dto.SelectedCategoryIds)
        };

        private async Task SaveUserCategoriesAsync(string userId, List<int> categoryIds)
        {
            var expertiseNames = await _context.Categories
                .Where(c => categoryIds.Contains(c.CategoryID))
                .Select(c => c.Name)
                .ToListAsync();

            var expertiseString = string.Join(",", expertiseNames);

            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Expertise = expertiseString;
                _context.Users.Update(user);
            }

            var userCategories = categoryIds.Select(categoryId => new UserCategory
            {
                UserId = userId,
                CategoryId = categoryId
            }).ToList();

            _context.UserCategories.AddRange(userCategories);
            await _context.SaveChangesAsync();
        }

        private async Task SendEmailConfirmationAsync(Users user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = _tokenHelper.GenerateConfirmationLink(user.Id, token);

            await _emailSender.SendEmailAsync(user.Email, "Confirm Your Email",
                $"Please confirm your account by clicking this link: {confirmationLink}");
        }
    }
}
