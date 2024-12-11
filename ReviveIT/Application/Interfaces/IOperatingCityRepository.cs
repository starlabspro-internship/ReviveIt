using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOperatingCityRepository : IRepository<OperatingCity>
    {
        Task<IEnumerable<OperatingCity>> GetOperatingCitiesAsync(string userId);
    }
}
