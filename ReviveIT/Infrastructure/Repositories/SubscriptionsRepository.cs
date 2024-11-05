using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class SubscriptionsRepository : Repository<Subscriptions>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(ApplicationDbContext context) : base(context) { }
    }
}
