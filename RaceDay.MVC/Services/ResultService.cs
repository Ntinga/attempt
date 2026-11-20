using System.Net.Http.Json;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public class ResultService : IResultService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ResultService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateResultAsync(
            ResultEntryViewModel model)
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayApi");

            var response =
                await client.PostAsJsonAsync(
                    "api/results",
                    model);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<ResultViewModel>>
            GetParticipantResultsAsync(int participantId)
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayApi");

            var results =
                await client.GetFromJsonAsync<List<dynamic>>
                ("api/results");

            return new List<ResultViewModel>();
        }
    }
}