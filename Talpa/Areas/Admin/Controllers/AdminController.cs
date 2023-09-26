using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TKDprogress.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [HttpGet]
        [Route("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
