using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserCategoryRepository : Repository<UserCategory>, IUserCategoryRepository
    {
        public UserCategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
