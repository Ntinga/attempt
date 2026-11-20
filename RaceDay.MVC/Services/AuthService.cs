using System.Net.Http.Json;
using RaceDay.MVC.Models;
using RaceDay.MVC.ViewModels;

namespace RaceDay.MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<LoginResponseModel?> LoginAsync(LoginViewModel model)
        {
            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var response = await client.PostAsJsonAsync(
                "api/auth/login",
                model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LoginResponseModel>();
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var response = await client.PostAsJsonAsync(
                "api/auth/register",
                model);

            return response.IsSuccessStatusCode;
        }
    }
}