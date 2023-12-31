﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Security.Claims;
using Talpa.Models;
using Talpa.Models.CreateModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_BLL.Services;

namespace Talpa.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class SuggestionController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IActivityService _activityService;
        private readonly IQuarterService _quarterService;
        private readonly IActivityDateService _activityDateService;
        private readonly ILimitationService _limitationService;
        private readonly IStringLocalizer<SuggestionController> _localizer;

        public SuggestionController(ISuggestionService suggestionService, IQuarterService quarterService, IActivityDateService activityDateService, IActivityService activityService, ILimitationService limitationService, IStringLocalizer<SuggestionController> localizer)
        {
            _suggestionService = suggestionService;
            _quarterService = quarterService;
            _activityDateService = activityDateService;
            _activityService = activityService;
            _limitationService = limitationService;
            _localizer = localizer;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            List<Suggestion> suggestions = await _suggestionService.GetPendingSuggestionsAsync(searchString);

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

            return View(suggestionViewModels);
        }

        public ActionResult Dates(string quarter, int suggestionId)
        {
            QuarterDay quarterDay = _quarterService.GetQuarterDays(quarter);

            QuarterDayViewModel quarterDayViewModel = new()
            {
                SuggestionId = suggestionId,
                Days = quarterDay.Days,
                SelectedQuarter = quarter,
            };

            return View(quarterDayViewModel);
        }

        public ActionResult Times(string selectedQuarter, string selectedDates, int suggestionId)
        {
            if (selectedDates == null)
            {
                TempData["ErrorMessage"] = _localizer["SelectOneOrMoreDays"].ToString();
                return RedirectToAction("Dates", new { quarter = selectedQuarter, suggestionId = suggestionId });
            }

            string[] dateStrings = selectedDates.Split(',');
            List<DateTime> selectedDateList = new();

            foreach (string dateString in dateStrings)
            {
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    selectedDateList.Add(date);
                }
            }

            selectedDateList.Sort();

            List<TimeInputViewModel> timeInputViewModel = selectedDateList.Select(selectedDate => new TimeInputViewModel
            {
                Date = selectedDate,
            }).ToList();

            TimeViewModel timeViewModel = new()
            {
                SuggestionId = suggestionId,
                TimeInputs = timeInputViewModel,
            };

            return View(timeViewModel);
        }

        /*public async Task<ActionResult> Details(int suggestionId)
        {
            Suggestion suggestion = await _suggestionService.GetSuggestionByIdAsync(suggestionId);

            if (suggestion.ErrorMessage == null)
            {
                SuggestionViewModel suggestionViewModel = new()
                {
                    Id = suggestion.Id,
                    Name = suggestion.Name,
                    Description = suggestion.Description,
                    ImageUrl = suggestion.ImageUrl,
                    Date = suggestion.Date,
                    ActivityState = suggestion.ActivityState,
                };

                List<Quarter> upcomingQuarters = _quarterService.GetUpcomingQuarters();

                List<QuarterViewModel> quarterViewModels = upcomingQuarters.Select(upcomingQuarter => new QuarterViewModel
                {
                    Name = upcomingQuarter.Name,
                    Quarters = upcomingQuarter.Quarters,
                }).ToList();

                SuggestionQuarterViewModel suggestionQuarterViewModel = new()
                {
                    Suggestion = suggestionViewModel,
                    Quarters = quarterViewModels,
                };

                return View(suggestionQuarterViewModel);
            }

            TempData["ErrorMessage"] = suggestion.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }*/

        public async Task<ActionResult> Details(int suggestionId)
        {
            Suggestion suggestion = await _suggestionService.GetSuggestionByIdAsync(suggestionId);

            if (suggestion.ErrorMessage == null)
            {
                SuggestionViewModel suggestionViewModel = new()
                {
                    Id = suggestion.Id,
                    Name = suggestion.Name,
                    Description = suggestion.Description,
                    ImageUrl = suggestion.ImageUrl,
                    Date = suggestion.Date,
                    ActivityState = suggestion.ActivityState,
                };

                List<Limitation> limitations = await _limitationService.GetLimitationsBySuggestionId(suggestionViewModel.Id);

                List<LimitationViewModel> limitationViewModels = limitations.Select(limitation => new LimitationViewModel
                {
                    Id = limitation.Id,
                    Name = limitation.Name,
                }).ToList();

                SuggestionLimitationViewModel suggestionLimitationViewModel = new()
                {
                    Suggestion = suggestionViewModel,
                    limitations = limitationViewModels,
                };

                return View(suggestionLimitationViewModel);
            }

            TempData["ErrorMessage"] = suggestion.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Times(List<TimeInputViewModel> timeInputs, int suggestionId)
        {
            if (!ModelState.IsValid)
            {
                return View(timeInputs);
            }

            List<ActivityDate> activityDates = timeInputs.Select(ti => new ActivityDate
            {
                SuggestionId = suggestionId,
                StartDate = ti.IsFullDay
                    ? ti.Date.Add(new TimeSpan(0, 0, 0))
                    : ti.Date.Add(new TimeSpan(ti.StartTimeHours, ti.StartTimeMinutes, 0)),
                EndDate = ti.IsFullDay
                    ? ti.Date.Add(new TimeSpan(23, 59, 0))
                    : ti.Date.Add(new TimeSpan(ti.EndTimeHours, ti.EndTimeMinutes, 0)),
            }).ToList();

            await _activityDateService.CreateActivityDates(activityDates);

            Suggestion suggestion = await _suggestionService.GetSuggestionByIdAsync(suggestionId);
            suggestion = await _activityService.CreateActivityAsync(suggestion);

            TempData["StatusMessage"] = _localizer["ActivityMadeDates"].ToString();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSuggestionViewModel suggestionViewModel, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(suggestionViewModel);
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Suggestion suggestion = new()
            {
                UserId = userId,
                Name = suggestionViewModel.Name,
                Description = suggestionViewModel.Description,
                ImageUrl = suggestionViewModel.ImageUrl,
            };

            suggestion = await _suggestionService.CreateSuggestionAsync(suggestion);

            if (suggestion.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = suggestion.ErrorMessage;
                return View(suggestionViewModel);
            }

            TempData["StatusMessage"] = _localizer["SuggestionCreated"].ToString();
            return RedirectToAction(nameof(Index));
        }

        // POST: SuggestionController/Edit/5
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

        // GET: SuggestionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SuggestionController/Delete/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Decline(int suggestionId, IFormCollection collection)
        {
            Suggestion suggestion = await _suggestionService.GetSuggestionByIdAsync(suggestionId);

            if (suggestion == null)
            {
                return View();
            }

            suggestion = await _suggestionService.DeclineSuggestionAsync(suggestion);

            if (suggestion.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = suggestion.ErrorMessage;
                return View();
            }

            TempData["StatusMessage"] = _localizer["SuggestionRejected"].ToString();
            return RedirectToAction(nameof(Index));
        }
    }
}
