using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talpa.Models;
using Talpa.Models.CreateModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Controllers
{
    [Authorize]
    public class SuggestionController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SuggestionController(ISuggestionService suggestionService, IWebHostEnvironment webHostEnvironment)
        {
            _suggestionService = suggestionService;
            _webHostEnvironment = webHostEnvironment;
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
                ImageUrl = suggestion.ImageUrl,
                Date = suggestion.Date,
                ActivityState = suggestion.ActivityState,
            }).ToList();

            return View(suggestionViewModels);
        }

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

                return View(suggestionViewModel);
            }

            TempData["ErrorMessage"] = suggestion.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSuggestionViewModel suggestionViewModel, IFormFile image)
        {
            string? fileName = await SaveImageAsync(image, _webHostEnvironment, null);

            if (!string.IsNullOrEmpty(fileName))
            {
                suggestionViewModel.ImageUrl = fileName;
            }
            else
            {
                return View(suggestionViewModel);
            }

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

            TempData["StatusMessage"] = "The suggestion was successfully created!";
            return RedirectToAction(nameof(Index));
        }

        // GET: SuggestionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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

        public async Task<string?> SaveImageAsync(IFormFile image, IWebHostEnvironment webHostEnvironment, string? existingImageUrl)
        {
            if (image != null && image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string webRootPath = webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, "img/suggestion", fileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }

            return existingImageUrl;
        }
    }
}
