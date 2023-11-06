using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Entities;

namespace Talpa.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IActivityService _activityService;
        private readonly IVoteService _voteService;

        public ActivityController(ISuggestionService suggestionService, IActivityService activityService, IVoteService voteService)
        {
            _suggestionService = suggestionService;
            _activityService = activityService;
            _voteService = voteService;
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
                Suggestions = activity.Suggestions?.Select(suggestion => new SuggestionViewModel
                {
                    Id = suggestion.Id,
                    Name = suggestion.Name,
                    Description = suggestion.Description,
                    ImageUrl = suggestion.ImageUrl,
                    Date = (DateTime?)suggestion.Date,
                    ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
                    VoteCount = _voteService.GetVoteCountBySuggestionAsync(suggestion.Id),
                }).ToList(),
                startTime = activity.startTime,
                endTime = activity.endTime
            }).ToList();

            return View(activityViewModels);
        }

        // GET: ActivityController/Details/5
        public async Task<ActionResult> Details(DateTime activityStartTime)
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

            Activity firstActivity = activities.FirstOrDefault(a => a.startTime == activityStartTime);

            if (firstActivity != null)
            {
                ActivityViewModel activityViewModel = new ActivityViewModel
                {
                    Suggestions = firstActivity.Suggestions?.Select(suggestion => new SuggestionViewModel
                    {
                        Id = suggestion.Id,
                        Name = suggestion.Name,
                        Description = suggestion.Description,
                        ImageUrl = suggestion.ImageUrl,
                        Date = (DateTime?)suggestion.Date,
                        ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
                        VoteCount = _voteService.GetVoteCountBySuggestionAsync(suggestion.Id),
                    }).ToList(),
                    startTime = firstActivity.startTime,
                    endTime = firstActivity.endTime
                };

                return View(activityViewModel);
            }
            else
            {
                return View("NoMatchingActivityView");
            }
        }
    }
}
