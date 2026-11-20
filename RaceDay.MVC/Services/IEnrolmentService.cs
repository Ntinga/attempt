using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public interface IEnrolmentService
    {
        Task<bool> EnrolAsync(EnrolmentCreateViewModel model);

        Task<List<dynamic>>GetEnrolmentsAsync();

    }
}