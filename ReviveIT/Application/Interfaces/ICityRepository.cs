using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task <IEnumerable<City>> GetAllCitiesAsync();
    }
}
