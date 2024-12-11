using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Cities
{
    public class GetCitiesFeature
    {
        private readonly ICityRepository _cityRepository;

        public GetCitiesFeature(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> ExecuteAsync()
        {
            return await _cityRepository.GetAllCitiesAsync();
        }
    }
}
