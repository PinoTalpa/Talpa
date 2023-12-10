using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ModelLayer.Models;
using System.Data;
using Talpa.Models;
using Talpa.Models.AdminModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Data;

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
        private readonly IStringLocalizer<ActivityController> _localizer;
        private readonly ApplicationDbContext _dbContext;

        public ActivityController(ISuggestionService suggestionService, IActivityService activityService, IQuarterService quarterService, IActivityDateService activityDateService, IVoteService voteService, IStringLocalizer<ActivityController> localizer, ApplicationDbContext dbContext)
        {
            _suggestionService = suggestionService;
            _activityService = activityService;
            _quarterService = quarterService;
            _activityDateService = activityDateService;
            _voteService = voteService;
            _localizer = localizer;
            _dbContext = dbContext;
        }

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

            if (suggestion != null && Date != DateTime.MinValue)
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

                ViewBag.Message = _localizer["ActivityAdded"].ToString();

                var suggestionIdsToDelete = new List<int> { SuggestionId, otherSuggestionId1, otherSuggestionId2 };

                var activitiesToDelete = _dbContext.ActivityDates
                    .Where(a => suggestionIdsToDelete.Contains(a.SuggestionId))
                    .ToList();

                _dbContext.ActivityDates.RemoveRange(activitiesToDelete);

                await _dbContext.SaveChangesAsync();


                // Remove suggestions
                var suggestionsToDelete = _dbContext.Suggestions.Where(s => suggestionIdsToDelete.Contains(s.Id)).ToList();
                _dbContext.Suggestions.RemoveRange(suggestionsToDelete);
                _dbContext.SaveChanges();

                TempData["StatusMessage"] = _localizer["ActivityAdded"].ToString();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = _localizer["ActivityNotFoundWithId"].ToString(); ;
            }

            if (Date == DateTime.MinValue)
            {
                TempData["ErrorMessage"] = _localizer["SelectDate"].ToString();
            }

            return RedirectToAction("GetActivityDateWithId", new { selectedSuggestionId = SuggestionId, otherSuggestionId1 = otherSuggestionId1, otherSuggestionId2 =  otherSuggestionId2 });
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

        //public IActionResult Details()
        //{
        //    return View();
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
                AdminActivityViewModel adminActivityViewModel = new AdminActivityViewModel
                {
                    ActivityId = firstActivity.Id,
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

                return View(adminActivityViewModel);
            }
            else
            {
                return Redirect(nameof(Index));
            }
        }

        public async Task<IActionResult> GetActivityDateWithId(int selectedSuggestionId, int otherSuggestionId1, int otherSuggestionId2, DateTime startTimeSuggestion)
        {
            // Assuming _activityService.GetActivitiesDateWithId returns a Task<List<ActivityDateDto>>
            var activitiesDateDto = await _activityDateService.GetActivityDatesWithId(selectedSuggestionId);


            // Check for errors
            //if (activitiesDateDto.Any(dto => dto.ErrorMessage != null))
            //{
            //    foreach (var dto in activitiesDateDto)
            //    {
            //        if (dto.ErrorMessage != null)
            //        {
            //            TempData["ErrorMessage"] = dto.ErrorMessage;
            //        }
            //    }

            //    return View(new List<AdminActivityViewModel>());
            //}

            // Convert ActivityDateDto to AdminActivityViewModel
            
            var activityViewModels = activitiesDateDto.Select(dto => new AdminActivityViewModel
            {
                Suggestions = new List<SuggestionViewModel>
                {
                    new SuggestionViewModel
                    {
                        Id = dto.Id,
                        // Map other properties as needed
                    }
                },
                OtherSuggestionId1 = otherSuggestionId1,
                OtherSuggestionId2 = otherSuggestionId2,
                SuggestionId = selectedSuggestionId,
                startTime = dto.StartDate,
                endTime = dto.EndDate
                // Add other properties as needed
            }).ToList();

            if (activityViewModels.Count == 0)
            {
                TempData["ErrorMessage"] = _localizer["ZeroVotes"].ToString();
                return RedirectToAction("Details", new { activityStartTime = startTimeSuggestion });
            }

            return View("ActivityDateViewModel", activityViewModels);
        }

        [HttpPost]
        //public async Task<ActivityDate> CreateSelectedActivity(int selectedSuggestionId)
        //{
        //    // Retrieve YourEntity items
        //    //var activityDateDto = await GetActivitiesWithSuggestionsAsync(selectedSuggestionId);

        //    //// Convert YourEntity items to ViewModel
        //    //var yourEntities = await ConvertActivityDateDtos(activityDateDtos);
        //    int id = selectedSuggestionId;
        //    List<ActivityDate> activityDate = await _activityDateService.CreateActivityDates(selectedSuggestionId);


        //    // Check for error messages
        //    if (activityDate.Any(entity => entity.ErrorMessage != null))
        //    {
        //        foreach (var entity in activityDate)
        //        {
        //            if (entity.ErrorMessage != null)
        //            {
        //                TempData["ErrorMessage"] = entity.ErrorMessage;
        //            }
        //        }

        //        return View(new List<ActivityDate>()); // Replace YourViewModel with the actual view model type
        //    }

        //    // Convert YourEntity items to ViewModel
        //    var viewModelList = yourEntities.Select(entity => new ActivityDate
        //    {
        //        // Map properties from YourEntity to YourViewModel
        //        Id = entity.Id,
        //        StartDate = entity.StartDate,
        //        EndDate = entity.EndDate,
        //        SuggestionId = entity.SuggestionId
        //        // Add other properties as needed
        //    }).ToList();

        //    return View(viewModelList);
        //}

        //public async Task<ActionResult> Details(DateTime activityStartTime)
        //{
        //    DateTime activityStartDate = activityStartTime;

        //    List<Activity> activities = await _activityService.GetActivitiesWithSuggestionsAsync();

        //    if (activities.Any(s => s.ErrorMessage != null))
        //    {
        //        foreach (Activity activity in activities)
        //        {
        //            if (activity.ErrorMessage != null)
        //            {
        //                TempData["ErrorMessage"] = activity.ErrorMessage;
        //            }
        //        }

        //        return View(new List<AdminActivityViewModel>());
        //    }

        //    Activity firstActivity = activities.FirstOrDefault(a => a.startTime.ToString("MM/dd/yyyy") == activityStartDate.ToString("MM/dd/yyyy"));

        //    if (firstActivity != null)
        //    {
        //        AdminActivityViewModel activityViewModel = new AdminActivityViewModel
        //        {
        //            Suggestions = firstActivity.Suggestions?.Select(suggestion => new SuggestionViewModel
        //            {
        //                Id = suggestion.Id,
        //                Name = suggestion.Name,
        //                Description = suggestion.Description,
        //                ImageUrl = suggestion.ImageUrl,
        //                Date = (DateTime?)suggestion.Date,
        //                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
        //                VoteCount = _voteService.GetVoteCountBySuggestionAsync(suggestion.Id),
        //            }).ToList(),
        //            startTime = firstActivity.startTime,
        //            endTime = firstActivity.endTime
        //        };

        //        return View(activityViewModel);
        //    }
        //    else
        //    {
        //        return Redirect(nameof(Index));
        //    }
        //}

        // GET: ActivityController/Create
        [HttpGet]
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
            if (suggestions.Length != 3)
            {
                TempData["ErrorMessage"] = _localizer["SelectThreeActivities"].ToString();
                return RedirectToAction(nameof(Index));
            }

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


            TempData["StatusMessage"] = _localizer["ActivityMadeWithDates"].ToString();
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

            TempData["StatusMessage"] = _localizer["ActivityRemoved"].ToString();
            return RedirectToAction(nameof(Index));
        }
    }
}
