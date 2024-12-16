using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User
{
    public class CompleteProfileFeature
    {
        private readonly UserManager<Users> _userManager;
        private readonly IApplicationDbContext _context;

        public CompleteProfileFeature(UserManager<Users> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<CompleteProfileResultDto> UpdateProfileAsync(string userIdClaim, CompleteProfileDto profileDto)
        {
            if (string.IsNullOrEmpty(userIdClaim))
                return CompleteProfileResultDto.FailureResult("User not authenticated.");

            var user = await _userManager.FindByIdAsync(userIdClaim);

            if (user == null)
                return CompleteProfileResultDto.FailureResult("User not found.");

            if (!string.IsNullOrEmpty(profileDto.Phone))
            {
                var existingUserWithPhone = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == profileDto.Phone);

                if (existingUserWithPhone != null && existingUserWithPhone.Id != user.Id)
                    return CompleteProfileResultDto.FailureResult("This phone number is already in use by another account.");

                user.PhoneNumber = profileDto.Phone;
            }

            if (!string.IsNullOrEmpty(profileDto.Description))
            {
                user.Description = profileDto.Description;
            }

            var existingOperatingCities = await _context.OperatingCities
                .Where(oc => oc.userId == userIdClaim)
                .ToListAsync();
            _context.OperatingCities.RemoveRange(existingOperatingCities);

            if (profileDto.Cities != null && profileDto.Cities.Any())
            {
                var operatingCities = profileDto.Cities.Select(cityId => new OperatingCity
                {
                    CityId = cityId,
                    userId = userIdClaim
                }).ToList();

                await _context.OperatingCities.AddRangeAsync(operatingCities);
            }

            user.CompletedProfile = !string.IsNullOrEmpty(user.PhoneNumber) &&
                                    !string.IsNullOrEmpty(user.Description) &&
                                    profileDto.Cities != null && profileDto.Cities.Any();

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return CompleteProfileResultDto.SuccessResult("Profile and cities updated successfully.");
            }

            return CompleteProfileResultDto.FailureResult("There was an error updating your profile.");
        }
    }
}