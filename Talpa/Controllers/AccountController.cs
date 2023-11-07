using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SampleMvcApp.ViewModels;
using System.Linq;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Talpa_BLL.Interfaces;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;
using Auth0.AuthenticationApi.Models;
using Talpa_BLL.Models;
using Microsoft.AspNetCore.Hosting;
using Talpa.Models;
using Talpa.Models.CreateModels;
using ModelLayer.Models;

namespace Talpa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AccountController(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("StoreUser", "Account"))
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<ActionResult> Profile()
        {
            string name = User.Identity.Name;
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

        // TODO: maak een index voor het ophalen van de gebruiker
        [Authorize]
/*        public async Task<ActionResult> Index()
        {

        }*/

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Update(UserProfileViewModel userProfileViewModel, IFormFile image)
        {
            string? fileName = await SaveImageAsync(image, _webHostEnvironment, userProfileViewModel.ProfileImage);
            userProfileViewModel.ProfileImage = fileName;

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User userProfile = new()
            {
                Id = userId,
                Name = userProfileViewModel.Name,
                ProfileImage = userProfileViewModel.ProfileImage,
            };

            userProfile = await _userService.UpdateUserAsync(userProfile);

            if (userProfile.ErrorMessage != null) {
                TempData["ErrorMessage"] = userProfile.ErrorMessage;
                return RedirectToAction("Profile", "Account");
            }

            TempData["StatusMessage"] = "The profile was updated successfully!";

            return RedirectToAction("Profile", "Account");
        }

        public async Task<string?> SaveImageAsync(IFormFile image, IWebHostEnvironment webHostEnvironment, string? existingImageUrl)
        {
            if (image != null && image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string webRootPath = webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, "img/profile", fileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }

            return existingImageUrl;
        }



        [Authorize]
        public async Task<ActionResult> StoreUser()
        {
            string accessToken = "";
            string apiUrl = "https://dev-qevf6pmo3qts8fwh.us.auth0.com/oauth/token";
            string requestBody = "grant_type=client_credentials&client_id=gcoE23abbXpG17MYNiiHSZsVirA7dCwB&client_secret=TP37t2k2r-cue7kdQmIBIq3JI_NxTQte46g6g-dCdqiAP2pQL6uYG21yQIcmb3h-&audience=https://dev-qevf6pmo3qts8fwh.us.auth0.com/api/v2/";

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<AccessTokenResponse>(responseBody);
                    accessToken = responseObj.AccessToken;

                    Console.WriteLine(accessToken);
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string apiUrl2 = $"https://dev-qevf6pmo3qts8fwh.us.auth0.com/api/v2/users/{userId}/roles";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl2);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var userClaims = (User.Identity as ClaimsIdentity);
                        userClaims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userClaims));
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                }
            }

            string name = User.Identity.Name;
            string emailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            await _userService.Login(userId, name, emailAddress);

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
