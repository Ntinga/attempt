using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    // API Controller handling race category management operations (Microsoft, 2024)
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        // Initializes a new instance of CategoriesController with database context (Microsoft, 2024)
        public CategoriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        // Retrieves all available race categories from the database (Microsoft, 2024)
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // Creates a new race category in the system (Microsoft, 2024)
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto request)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                EventId = request.EventId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category created successfully", categoryId = category.CategoryId });
        }
    }
}