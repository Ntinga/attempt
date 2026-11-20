using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Filters;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    [ParticipantOnly]
    public class ParticipantController : Controller
    {
        private readonly IEventService _eventService;

        private readonly IResultService _resultService;
        private readonly IEnrolmentService _enrolmentService;

        public ParticipantController(IEventService eventService, IEnrolmentService enrolmentService, IResultService resultService)
        {
            _eventService = eventService;
            _enrolmentService = enrolmentService;
            _resultService = resultService;
        }

        public async Task<IActionResult> Index(string? searchTerm, decimal? distanceKm)
        {
            var events = await _eventService.GetEventsAsync();

            var categories = await _eventService.GetCategoriesAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                events = events.Where(e =>
                    e.EventName.Contains(searchTerm,
                    StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (distanceKm.HasValue)
            {
                var matchingEventIds = categories
                    .Where(c => c.DistanceKM == distanceKm.Value)
                    .Select(c => c.EventId)
                    .Distinct()
                    .ToList();

                events = events
                    .Where(e => matchingEventIds.Contains(e.EventId))
                    .ToList();
            }

            ViewBag.Categories = categories;

            return View(events);
        }

            public async Task<IActionResult> Details(int id)
            {
                var raceEvent =
                    await _eventService.GetEventByIdAsync(id);

                var categories =
                    await _eventService.GetCategoriesAsync();

                var vm = new EventDetailsViewModel
                {
                    Event = raceEvent!,
                    Categories = categories
                        .Where(c => c.EventId == id)
                        .ToList()
                };

            return View(vm);
            }

        [HttpPost]
        public async Task<IActionResult> Enrol(int categoryId)
        {
            var participantId =
                HttpContext.Session.GetInt32("UserId");

            if (participantId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var enrolment =
                new EnrolmentCreateViewModel
                {
                    CategoryId = categoryId,
                    ParticipantId = participantId.Value
                };

            await _enrolmentService.EnrolAsync(
                enrolment);

            TempData["Success"] =
                "Successfully enrolled!";

            return RedirectToAction(
                "MyEnrolments");
        }
        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> MyResults()
        {
            return View(new List<ResultViewModel>());
        }

    }
}