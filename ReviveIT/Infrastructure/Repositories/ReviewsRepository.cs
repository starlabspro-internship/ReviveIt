using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ReviewsRepository : Repository<Reviews>, IReviewsRepository
    {
        public ReviewsRepository(ApplicationDbContext context) : base(context) { }
    }
}
