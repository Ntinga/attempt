using System.Net.Http.Json;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public class CategoryService
        : ICategoryService
    {
        private readonly IHttpClientFactory
            _httpClientFactory;

        public CategoryService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory =
                httpClientFactory;
        }

        public async Task<List<CategoryDto>>
            GetCategoriesAsync()
        {
            var client =
                _httpClientFactory
                    .CreateClient("RaceDayApi");

            return await client
                       .GetFromJsonAsync<List<CategoryDto>>
                       ("api/categories")
                   ?? new();
        }

        public async Task<bool>
            CreateCategoryAsync(
                CategoryCreateViewModel model)
        {
            var client =
                _httpClientFactory
                    .CreateClient("RaceDayApi");

            var response =
                await client.PostAsJsonAsync(
                    "api/categories",
                    model);

            return response.IsSuccessStatusCode;
        }
    }
}