using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public interface IEventService
    {
        Task<List<EventDto>> GetEventsAsync();

        Task<EventDto?> GetEventByIdAsync(int id);

        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<bool> CreateEventAsync(EventCreateViewModel model);
    }
}