using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IApplicationDbContext _context;

    public RefreshTokenRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddOrUpdateRefreshTokenAsync(UserRefreshToken refreshToken)
    {
        var existingRefreshToken = await _context.UserRefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == refreshToken.UserId);

        if (existingRefreshToken != null)
        {
            existingRefreshToken.Token = refreshToken.Token;
            existingRefreshToken.ExpiresOn = refreshToken.ExpiresOn;
            _context.UserRefreshTokens.Update(existingRefreshToken);
        }
        else
        {
            await _context.UserRefreshTokens.AddAsync(refreshToken);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<UserRefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.UserRefreshTokens
            .Where(rt => rt.Token == token)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> RemoveRefreshTokenAsync(string userId)
    {
        var refreshToken = await _context.UserRefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (refreshToken != null)
        {
            _context.UserRefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var refreshToken = await GetByTokenAsync(token);
        if (refreshToken != null)
        {
            _context.UserRefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
