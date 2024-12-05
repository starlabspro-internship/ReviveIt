using Domain.Entities;

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