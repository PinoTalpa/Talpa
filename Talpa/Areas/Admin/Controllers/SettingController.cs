using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Talpa.Models.AdminModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IStringLocalizer<SettingController> _localizer;

        public SettingController(ISettingsService settingsService, IStringLocalizer<SettingController> localizer)
        {
            _settingsService = settingsService;
            _localizer = localizer;
        }

        public async Task<ActionResult> Index()
        {
            Settings settings = await _settingsService.GetSettings();

            SettingsViewModel settingsViewModel = new()
            {
                Id = settings.Id,
                PrimaryColor = settings.PrimaryColor,
                SecondaryColor = settings.SecondaryColor,
                BackgroundColor = settings.BackgroundColor,
            };         

            return View(settingsViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(SettingsViewModel settingsViewModel)
        {
            if (ModelState.IsValid)
            {
                Settings settings = new()
                {
                    Id = settingsViewModel.Id,
                    PrimaryColor = settingsViewModel.PrimaryColor,
                    SecondaryColor = settingsViewModel.SecondaryColor,
                    BackgroundColor = settingsViewModel.BackgroundColor,
                };

                settings = await _settingsService.UpdateSettings(settings);

                Response.Cookies.Append("PrimaryColor", settings.PrimaryColor);
                Response.Cookies.Append("SecondaryColor", settings.SecondaryColor);
                Response.Cookies.Append("BackgroundColor", settings.BackgroundColor);

                TempData["StatusMessage"] = _localizer["SettingChanged"].ToString();
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = _localizer["SettingsError"].ToString();
            return View(settingsViewModel);
        }
    }
}
