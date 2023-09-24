using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IQuarterService _quarterService;

        public SuggestionController(ISuggestionService suggestionService, IQuarterService quarterService)
        {
            _suggestionService = suggestionService;
            _quarterService = quarterService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            List<Suggestion> suggestions = await _suggestionService.GetSuggestionsAsync(searchString);

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
                Date = suggestion.Date,
                ActivityState = suggestion.ActivityState,
            }).ToList();

            return View(suggestionViewModels);
        }

        public ActionResult Create()
        {
            QuarterDay quarterDay = _quarterService.GetQuarterDays();

            QuarterDayViewModel quarterDayViewModel = new()
            {
                Days = quarterDay.Days,
            };

            return View(quarterDayViewModel);
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
                Description = suggestionViewModel.Description
            };

            suggestion = await _suggestionService.CreateSuggestionAsync(suggestion);

            if (suggestion.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = suggestion.ErrorMessage;
                return View(suggestionViewModel);
            }

            TempData["StatusMessage"] = "The suggestion was successfully declined!";
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

            TempData["StatusMessage"] = "The suggestion was successfully created!";
            return RedirectToAction(nameof(Index));
        }
    }
}
