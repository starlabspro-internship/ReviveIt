using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ServicesRepository : Repository<Service>, IServicesRepository
    {
        public ServicesRepository(ApplicationDbContext context) : base(context) { }
    }
}
