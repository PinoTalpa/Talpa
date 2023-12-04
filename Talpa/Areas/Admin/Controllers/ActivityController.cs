using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talpa.Models;
using Talpa.Models.AdminModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_BLL.Services;

namespace Talpa.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ActivityController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IActivityService _activityService;
        private readonly IQuarterService _quarterService;
        private readonly IActivityDateService _activityDateService;
        private readonly IVoteService _voteService;

        public ActivityController(ISuggestionService suggestionService, IActivityService activityService, IQuarterService quarterService, IActivityDateService activityDateService, IVoteService voteService)
        {
            _suggestionService = suggestionService;
            _activityService = activityService;
            _quarterService = quarterService;
            _activityDateService = activityDateService;
            _voteService = voteService;
        }

<<<<<<< Updated upstream
=======
        //public async Task<ActionResult> SaveChosenActivity(int SuggestionId, int OtherSuggestionId1, int OtherSuggestionId2, DateTime Date)
        //{
        //    // Assuming you have a DbContext named YourDbContext
        //    using (var dbContext = new _dbContext.Suggestions())
        //    {
        //        // Find the suggestion by Id
        //        var suggestion = await dbContext.Suggestions.FindAsync(SuggestionId);

        //        if (suggestion != null)
        //        {
        //            // Create a new ChosenSuggestion based on the Suggestion
        //            var chosenSuggestion = new ChosenSuggestion
        //            {
        //                SuggestionId = suggestion.Id,
        //                // Copy other properties as needed...
        //                Date = Date,
        //                // Set OtherSuggestionId1 and OtherSuggestionId2 as needed...
        //            };

        //            // Add the new ChosenSuggestion to the ChosenSuggestions table
        //            dbContext.ChosenSuggestions.Add(chosenSuggestion);

        //            // Save changes to the database
        //            await dbContext.SaveChangesAsync();
        //        }
        //    }

        //    return RedirectToAction();
        //}


        public async Task<ActionResult> SaveChosenActivity(int SuggestionId, DateTime Date, int otherSuggestionId1, int otherSuggestionId2)
        {
            var suggestion = await _dbContext.Suggestions.FindAsync(SuggestionId);

            if (suggestion != null)
            {
                var chosenSuggestion = new ChosenSuggestion
                {
                    UserId = suggestion.UserId,
                    Name = suggestion.Name,
                    Description = suggestion.Description,
                    ImageUrl = suggestion.ImageUrl,
                    Date = Date,
                    ActivityState = suggestion.ActivityState
                };

                _dbContext.ChosenSuggestions.Add(chosenSuggestion);

                await _dbContext.SaveChangesAsync();

                ViewBag.Message = "ChosenSuggestion added successfully.";
            }
            else
            {
                ViewBag.Message = "Suggestion not found with the given ID.";
            }

            var suggestionIdsToDelete = new List<int> { SuggestionId, otherSuggestionId1, otherSuggestionId2 };

            var activitiesToDelete = _dbContext.ActivityDates
                .Where(a => suggestionIdsToDelete.Contains(a.SuggestionId))
                .ToList();

            _dbContext.ActivityDates.RemoveRange(activitiesToDelete);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

>>>>>>> Stashed changes
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

                return View(new List<AdminActivityViewModel>());
            }

            List<AdminActivityViewModel> activityViewModels = activities.Select(activity => new AdminActivityViewModel
            {
                Suggestions = activity.Suggestions?.Select(suggestion => new SuggestionViewModel
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
        //public async Task<ActionResult> Details(int activityId)
        //{
        //    Activity activity = await _activityService.GetActivityByIdAsync(activityId);

        //    if (activity.ErrorMessage == null)
        //    {
        //        AdminActivityViewModel activityViewModel = new()
        //        {
        //            Id = activity.Id,
        //            Name = activity.Name,
        //            Description = activity.Description,
        //            ImageUrl = activity.ImageUrl,
        //            Date = activity.Date,
        //            ActivityState = (ModelLayer.Enums.ActivityState)activity.ActivityState,
        //        };

        //        return View(activityViewModel);
        //    }

        //    TempData["ErrorMessage"] = activity.ErrorMessage;
        //    return RedirectToAction(nameof(Index));
        //}

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

                return View(new List<AdminActivityViewModel>());
            }

            Activity firstActivity = activities.FirstOrDefault(a => a.startTime.ToString("MM/dd/yyyy") == activityStartDate.ToString("MM/dd/yyyy"));

            if (firstActivity != null)
            {
                AdminActivityViewModel activityViewModel = new AdminActivityViewModel
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

        // GET: ActivityController/Create
        public async Task<ActionResult> Create()
        {
            List<Suggestion> suggestions = await _suggestionService.GetPendingSuggestionsAsync("");

            if (suggestions.Any(s => s.ErrorMessage != null))
            {
                foreach (Suggestion suggestion in suggestions)
                {
                    if (suggestion.ErrorMessage != null)
                    {
                        TempData["ErrorMessage"] = suggestion.ErrorMessage;
                    }
                }

                return View(new List<SuggestionViewModel>());
            }

            List<SuggestionViewModel> suggestionViewModels = suggestions.Select(suggestion => new SuggestionViewModel
            {
                Id = suggestion.Id,
                Name = suggestion.Name,
                Description = suggestion.Description,
                ImageUrl = suggestion.ImageUrl,
                Date = suggestion.Date,
                ActivityState = suggestion.ActivityState,
            }).ToList();

            List<Quarter> upcomingQuarters = _quarterService.GetUpcomingQuarters();

            List<QuarterViewModel> quarterViewModels = upcomingQuarters.Select(upcomingQuarter => new QuarterViewModel
            {
                Name = upcomingQuarter.Name,
                Quarters = upcomingQuarter.Quarters,
            }).ToList();

            SuggestionQuarterViewModel suggestionQuarterViewModel = new()
            {
                Suggestions = suggestionViewModels,
                Quarters = quarterViewModels,
            };

            return View(suggestionQuarterViewModel);
        }

        public ActionResult Times(string[] suggestions, DateTime eventStart, DateTime eventEnd)
        {
            List<int> selectedSuggestionIds = new List<int>();

            foreach (var suggestion in suggestions)
            {
                if (int.TryParse(suggestion, out int suggestionId))
                {
                    selectedSuggestionIds.Add(suggestionId);
                }
            }

            List<DateTime> allDatesInRange = new();
            for (DateTime date = eventStart; date <= eventEnd; date = date.AddDays(1))
            {
                allDatesInRange.Add(date);
            }

            List<DateViewModel> allDates = allDatesInRange.Select(date => new DateViewModel
            {
                Date = date,
                IsChecked = false
            }).ToList();

            EventViewModel eventViewModel = new()
            {
                SelectedSuggestionIds = selectedSuggestionIds,
                AllDatesInRange = allDates,
            };

            return View(eventViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Times(EventViewModel eventViewModel)
        {
            foreach (int selectedSuggestionId in eventViewModel.SelectedSuggestionIds)
            {
                List<ActivityDate> activityDates = eventViewModel.AllDatesInRange
                    .Where(ad => ad.IsChecked)
                    .Select(ad => new ActivityDate
                    {
                        SuggestionId = selectedSuggestionId,
                        StartDate = ad.Date,
                        EndDate = ad.Date,
                    })
                    .ToList();

                await _activityDateService.CreateActivityDates(activityDates);

                Suggestion suggestion = await _suggestionService.GetSuggestionByIdAsync(selectedSuggestionId);
                suggestion = await _activityService.CreateActivityAsync(suggestion);
            }
            

            TempData["StatusMessage"] = "The activity was successfully made with the dates!";
            return RedirectToAction(nameof(Index));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(int activityId, IFormCollection collection)
        {
            Suggestion activity = await _suggestionService.GetSuggestionByIdAsync(activityId);

            if (activity == null)
            {
                return View();
            }

            activity = await _suggestionService.DeclineSuggestionAsync(activity);

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
