using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories
{
    public class GetCategoriesFeature
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesFeature(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> ExecuteAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}