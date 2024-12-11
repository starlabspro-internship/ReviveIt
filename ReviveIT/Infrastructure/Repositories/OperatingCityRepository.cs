using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OperatingCityRepository : Repository<OperatingCity>, IOperatingCityRepository
    {
        public OperatingCityRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<OperatingCity>> GetOperatingCitiesAsync(string userId)
        {
            return await _context.Set<OperatingCity>()
                .Where(tc => tc.userId == userId)
                .Include(tc => tc.City)
                .ToListAsync();
        }
    }
}
