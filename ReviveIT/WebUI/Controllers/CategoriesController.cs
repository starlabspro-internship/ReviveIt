using Application.Features.Categories;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly GetCategoriesFeature _getCategoriesFeature;

        public CategoriesController(GetCategoriesFeature getCategoriesFeature)
        {
            _getCategoriesFeature = getCategoriesFeature;
        }

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _getCategoriesFeature.ExecuteAsync();
            return Ok(categories);
        }
    }
}
