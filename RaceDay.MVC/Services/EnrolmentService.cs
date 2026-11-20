using System.Net.Http.Json;
using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public class EnrolmentService : IEnrolmentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EnrolmentService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> EnrolAsync(EnrolmentCreateViewModel model)
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayApi");

            var response =
                await client.PostAsJsonAsync(
                    "api/enrolments",
                    model);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<dynamic>>GetEnrolmentsAsync()
        {
            var client =
                _httpClientFactory.CreateClient("RaceDayApi");

            return await client.GetFromJsonAsync<List<dynamic>>
                ("api/enrolments")
                ?? new List<dynamic>();
        }
    }
}