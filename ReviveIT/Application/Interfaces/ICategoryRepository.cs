using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetAllAsync();
    }
}
