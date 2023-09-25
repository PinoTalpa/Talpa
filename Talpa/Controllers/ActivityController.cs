using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Controllers
{
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

                return View(new List<ActivityViewModel>());
            }

            List<ActivityViewModel> activityViewModels = activities.Select(activity => new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Date = activity.Date,
                ActivityState = activity.ActivityState,
            }).ToList();

            return View(activityViewModels);
        }

        // GET: ActivityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // POST: ActivityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
