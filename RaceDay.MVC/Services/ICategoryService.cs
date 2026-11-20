using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>>
            GetCategoriesAsync();

        Task<bool>
            CreateCategoryAsync(
                CategoryCreateViewModel model);
    }
}