using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ModelLayer.Models;
using SampleMvcApp.ViewModels;
using System.Security.Claims;
using Talpa.Controllers;
using Talpa_BLL.Interfaces;

namespace Talpa.Components
{
    public class MiniProfileAdminViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public MiniProfileAdminViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            string emailAddress = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            UserProfileViewModel userProfileViewModel = new UserProfileViewModel
            {
                Name = "Placeholder Name",
                EmailAddress = emailAddress,
                ProfileImage = "Placeholder Image"
            };

            Task.Run(async () =>
            {
                UserDto user = await _userService.GetUserAsync(userId);

                if (user != null)
                {
                    userProfileViewModel.Name = user.Name;
                    userProfileViewModel.ProfileImage = user.ProfileImage;
                }
            }).Wait();

            return View(userProfileViewModel);
        }
    }
}
