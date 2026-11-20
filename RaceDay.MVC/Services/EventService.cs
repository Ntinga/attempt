using System.Net.Http.Json;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public class EventService : IEventService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<EventDto>> GetEventsAsync()
        {
            var client = _httpClientFactory.CreateClient("RaceDayApi");

            return await client.GetFromJsonAsync<List<EventDto>>
                ("api/events")
                ?? new List<EventDto>();
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var client = _httpClientFactory.CreateClient("RaceDayApi");

            return await client.GetFromJsonAsync<List<CategoryDto>>
                ("api/categories")
                ?? new List<CategoryDto>();
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayApi");

            return await client.GetFromJsonAsync<EventDto>
                ($"api/events/{id}");
        }

        public async Task<bool> CreateEventAsync(EventCreateViewModel model)
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayApi");

            var response =
                await client.PostAsJsonAsync(
                    "api/events",
                    model);

            return response.IsSuccessStatusCode;
        }
    }
}