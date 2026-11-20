using RaceDay.MVC.Models;
using RaceDay.MVC.ViewModels;

namespace RaceDay.MVC.Services
{
    public interface IAuthService
    {
        Task<LoginResponseModel?> LoginAsync(LoginViewModel model);

        Task<bool> RegisterAsync(RegisterViewModel model);
    }
}