using Domain.Entities;

namespace Application.Helpers
{
    public interface ITokenHelper
    {
        string GenerateToken(Users user);
    }
}
