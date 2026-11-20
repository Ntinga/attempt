using RaceDay.MVC.Models;

namespace RaceDay.MVC.Services
{
    public interface IResultService
    {
        Task<List<ResultViewModel>>GetParticipantResultsAsync(int participantId);
        Task<bool> CreateResultAsync(ResultEntryViewModel model);
    }
}