using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Application.Helpers
{
    public static class UserHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static IConfiguration _configuration;

        public static void Configure(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public static string GetUserId()
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                Console.WriteLine("No HTTP context available.");
                throw new UnauthorizedException("No HTTP context available.");
            }

            var userIdClaim = httpContext.User.FindFirst("UserId");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                Console.WriteLine("User ID not found in token.");
                throw new UnauthorizedException("User ID not found in the current context.");
            }

            Console.WriteLine($"User ID found in token: {userIdClaim.Value}");
            return userIdClaim.Value;
        }
    }
}
