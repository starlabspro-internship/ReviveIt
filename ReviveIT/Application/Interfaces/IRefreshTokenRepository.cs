using Domain.Entities;

namespace Application.Interfaces
{ 
    public interface IRefreshTokenRepository
    {
        Task AddOrUpdateRefreshTokenAsync(UserRefreshToken refreshToken);
        Task<UserRefreshToken?> GetByTokenAsync(string token);
        Task<bool> RemoveRefreshTokenAsync(string userId);
        Task RevokeRefreshTokenAsync(string token);
    }
}
