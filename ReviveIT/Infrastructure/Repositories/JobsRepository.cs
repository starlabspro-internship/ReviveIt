using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class JobsRepository : Repository<Jobs>, IJobsRepository
    {
        public JobsRepository(ApplicationDbContext context) : base(context) { }
    }
}
