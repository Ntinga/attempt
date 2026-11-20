using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RaceDay.MVC.Filters;
using RaceDay.MVC.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    [OrganiserOnly]
    public class OrganiserController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEnrolmentService _enrolmentService;
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        private readonly IResultService _resultService;

        public OrganiserController(
            IEventService eventService,
            IEnrolmentService enrolmentService,
            IFileService fileService,
            ICategoryService categoryService,
            IResultService resultService)
        {
            _eventService = eventService;
            _enrolmentService = enrolmentService;
            _fileService = fileService;
            _categoryService = categoryService;
            _resultService = resultService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var events =
                await _eventService.GetEventsAsync();

            var enrolments =
                await _enrolmentService.GetEnrolmentsAsync();

            var vm = new DashboardViewModel
            {
                TotalEvents = events.Count(),

                TotalEnrolments =
                    enrolments.Count(),

                UpcomingEvents = events
                    .Where(e => e.EventDate > DateTime.Now)
                    .OrderBy(e => e.EventDate)
                    .ToList()
            };

            return View(vm);
        }

        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(
            EventCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (model.BannerImage != null)
                {
                    model.BannerUrl =
                        await _fileService.UploadFileAsync(
                            model.BannerImage);
                }

                model.OrganiserId =
                    HttpContext.Session.GetInt32("UserId")
                    ?? 0;

                var success =
                    await _eventService
                        .CreateEventAsync(model);

                if (success)
                {
                    TempData["Success"] =
                        "Event created successfully.";

                    return RedirectToAction(
                        nameof(Dashboard));
                }

                TempData["Error"] =
                    "Unable to create event.";
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Unable to create event at this time.";
            }

            return View(model);
        }

        public async Task<IActionResult> ManageCategories()
        {
            var categories =
                await _categoryService
                    .GetCategoriesAsync();

            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(
            CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success =
                    await _categoryService
                        .CreateCategoryAsync(model);

                if (success)
                {
                    TempData["Success"] =
                        "Category created successfully.";

                    return RedirectToAction(
                        nameof(ManageCategories));
                }

                TempData["Error"] =
                    "Unable to create category.";
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Unable to create category at this time.";
            }

            return View(model);
        }

        public async Task<IActionResult> EventEnrolments()
        {
            var enrolments =
                await _enrolmentService
                    .GetEnrolmentsAsync();

            return View(enrolments);
        }

        public IActionResult EnterResult(
            int enrolmentId)
        {
            var model =
                new ResultEntryViewModel
                {
                    EnrolmentId = enrolmentId
                };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EnterResult(
            ResultEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success =
                    await _resultService
                        .CreateResultAsync(model);

                if (success)
                {
                    TempData["Success"] =
                        "Result published successfully.";

                    return RedirectToAction(
                        nameof(EventEnrolments));
                }

                TempData["Error"] =
                    "Unable to publish result.";
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Unable to publish result at this time.";
            }

            return View(model);
        }
    }
}