// MockTokenHelper.cs
using Domain.Entities;
using ReviveIt.test.Helpers;

namespace ReviveIt.test.Helpers
{
    public class MockTokenHelper 
    {
        public string GenerateToken(Users user)
        {
            return "mock-jwt-token";
        }

        public string GenerateRefreshToken()
        {
            return "mock-refresh-token";
        }
    }
}
