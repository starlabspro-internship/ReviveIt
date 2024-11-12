using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Constants;
using System.Security.Cryptography;

namespace Application.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[ConfigurationConstant.Key]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Email", user.Email),
                new Claim("UserId", user.Id),
                new Claim("FullName", user.FullName ?? string.Empty),
                new Claim("CreatedAt", user.CreatedAt.ToString("O")),
                new Claim("CompanyName", user.CompanyName ?? string.Empty),
                new Claim("CompanyAddress", user.CompanyAddress ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration[ConfigurationConstant.Issuer],
                audience: _configuration[ConfigurationConstant.Audience],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration[ConfigurationConstant.ExpiresInMinutes])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
