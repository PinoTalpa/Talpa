using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SampleMvcApp.ViewModels;
using System.Linq;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Talpa_DAL.Entities;
using Talpa_BLL.Interfaces;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;
using Auth0.AuthenticationApi.Models;

namespace Talpa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
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

            return View(new UserProfileViewModel()
            {
                Name = User.Identity.Name,
                EmailAddress = emailAddress,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
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
