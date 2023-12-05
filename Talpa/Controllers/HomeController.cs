using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ModelLayer.Models;
using SampleMvcApp.ViewModels;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Claims;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_DAL.Data;

namespace Talpa.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer, IUserService userService, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _localizer = localizer;
            _userService = userService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetSettings()
        {
            var settings = _dbContext.Settings.FirstOrDefault();

            Response.Cookies.Append("PrimaryColor", settings.PrimaryColor);
            Response.Cookies.Append("SecondaryColor", settings.SecondaryColor);
            Response.Cookies.Append("BackgroundColor", settings.BackgroundColor);

            return Json(settings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> MiniProfile()
        {
            string emailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            UserDto user = await _userService.GetUserAsync(userId);

            return View(new UserProfileViewModel()
            {
                Name = user.Name,
                EmailAddress = emailAddress,
                ProfileImage = user.ProfileImage
            });
        }

        [HttpPost]
        public ActionResult SetCulture(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Json(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}