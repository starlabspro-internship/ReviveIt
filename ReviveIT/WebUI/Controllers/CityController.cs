using Application.Features.Cities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly GetCitiesFeature _getCitiesFeature;

        public CityController(GetCitiesFeature getCitiesFeature)
        {
            _getCitiesFeature = getCitiesFeature;
        }

        [HttpGet("getCities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _getCitiesFeature.ExecuteAsync();
            return Ok(cities);
        }
    }
}
