using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context){}

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _context.Set<City>().ToListAsync();
        }
    }
}