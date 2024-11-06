using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class JobApplicationsRepository : Repository<JobApplication>, IJobApplicationsRepository
    {
        public JobApplicationsRepository(ApplicationDbContext context) : base(context) { }
    }
}
