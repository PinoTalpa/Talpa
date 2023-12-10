using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ActivityController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IActivityService _activityService;
        private readonly IVoteService _voteService;
        private readonly IStringLocalizer<VoteController> _localizer;

        public ActivityController(ISuggestionService suggestionService, IActivityService activityService, IVoteService voteService, IStringLocalizer<VoteController> localizer)
        {
            _suggestionService = suggestionService;
            _activityService = activityService;
            _voteService = voteService;
            _localizer = localizer;
        }

        public async Task<ActionResult> PlannedActivities()
        {
            List<Suggestion> activities = await _activityService.GetActivitiesAsync("");

            if (activities.Any(s => s.ErrorMessage != null))
            {
                foreach (var activity in activities.Where(activity => activity.ErrorMessage != null))
                {
                    TempData["ErrorMessage"] = activity.ErrorMessage;
                }

                return View(new List<PlannedActivityViewModel>());
            }
            
            List<PlannedActivityViewModel> plannedActivityViewModel = activities.Select(activity => new PlannedActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ImageUrl = activity.ImageUrl,
                Date = (DateTime)activity.Date,
            }).ToList();

            return View(plannedActivityViewModel);
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
            DateTime activityStartDate = activityStartTime;

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

            Activity firstActivity = activities.FirstOrDefault(a => a.startTime.ToString("MM/dd/yyyy") == activityStartDate.ToString("MM/dd/yyyy"));

            if (firstActivity != null)
            {
                foreach (Suggestion suggestion in firstActivity.Suggestions)
                {
                    string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    Vote vote = new()
                    {
                        UserId = userId,
                        SuggestionId = suggestion.Id,
                    };

                    Vote existingVote = await _voteService.getExistingVoteAsync(vote);

                    if (existingVote.Id != 0)
                    {
                        TempData["ErrorMessage"] = _localizer["AlreadyVoted"].ToString();
                        return RedirectToAction(nameof(Index), "Activity");
                    }
                }

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
                return Redirect(nameof(Index));
            }
        }
    }
}
