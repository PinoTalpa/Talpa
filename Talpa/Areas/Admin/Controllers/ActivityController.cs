using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talpa.Models;
using Talpa.Models.AdminModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ActivityController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IActivityService _activityService;

        public ActivityController(ISuggestionService suggestionService, IActivityService activityService)
        {
            _suggestionService = suggestionService;
            _activityService = activityService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            List<Activity> activities = await _activityService.GetActivitiesAsync(searchString);

            if (activities.Any(s => s.ErrorMessage != null))
            {
                foreach (Activity activity in activities)
                {
                    if (activity.ErrorMessage != null)
                    {
                        TempData["ErrorMessage"] = activity.ErrorMessage;
                    }
                }

                return View(new List<AdminActivityViewModel>());
            }

            List<AdminActivityViewModel> activityViewModels = activities.Select(activity => new AdminActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Date = activity.Date,
                ImageUrl = activity.ImageUrl,
                ActivityState = (ModelLayer.Enums.ActivityState)activity.ActivityState,
            }).ToList();

            return View(activityViewModels);
        }

        // GET: ActivityController/Details/5
        public async Task<ActionResult> Details(int activityId)
        {
            Activity activity = await _activityService.GetActivityByIdAsync(activityId);

            if (activity.ErrorMessage == null)
            {
                AdminActivityViewModel activityViewModel = new()
                {
                    Id = activity.Id,
                    Name = activity.Name,
                    Description = activity.Description,
                    ImageUrl = activity.ImageUrl,
                    Date = activity.Date,
                    ActivityState = (ModelLayer.Enums.ActivityState)activity.ActivityState,
                };

                return View(activityViewModel);
            }

            TempData["ErrorMessage"] = activity.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        // GET: ActivityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActivityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int activityId, IFormCollection collection)
        {
            Activity activity = await _activityService.GetActivityByIdAsync(activityId);

            if (activity == null)
            {
                return View();
            }

            activity = await _activityService.RemoveActivityAsync(activity);

            if (activity.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = activity.ErrorMessage;
                return View();
            }

            TempData["StatusMessage"] = "The activity was successfully removed!";
            return RedirectToAction(nameof(Index));
        }
    }
}
