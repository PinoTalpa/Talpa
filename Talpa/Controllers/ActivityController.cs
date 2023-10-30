using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IActivityService _activityService;

        public ActivityController(ISuggestionService suggestionService, IActivityService activityService)
        {
            _suggestionService = suggestionService;
            _activityService = activityService;
        }

        public async Task<ActionResult> Index()
        {
            List<Activity> activities = await _activityService.GetActivitiesWithSuggestionsAsync();

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
                Suggestions = activity.Suggestions?.Select(suggestion => new Suggestion
                {
                    Id = suggestion.Id,
                    Name = suggestion.Name,
                    Description = suggestion.Description,
                    ImageUrl = suggestion.ImageUrl,
                    Date = (DateTime?)suggestion.Date,
                    ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
                }).ToList(),
                startTime = activity.startTime,
                endTime = activity.endTime
            }).ToList();

            return View(activityViewModels);
        }

        // GET: ActivityController/Details/5
        public async Task<ActionResult> Details(int activityId)
        {
            Activity activity = null; // await _activityService.GetActivityByIdAsync(activityId);

            //if (activity.ErrorMessage == null)
            //{
            //    ActivityViewModel activityViewModel = new()
            //    {
            //        Id = activity.Id,
            //        Name = activity.Name,
            //        Description = activity.Description,
            //        ImageUrl = activity.ImageUrl,
            //        Date = activity.Date,
            //        ActivityState = activity.ActivityState,
            //    };

            //    return View(activityViewModel);
            //}

            TempData["ErrorMessage"] = activity.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
