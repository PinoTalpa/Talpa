using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System.Security.Claims;
using Talpa.Models;
using Talpa.Models.CreateModels;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;


namespace Talpa.Controllers
{
    public class LimitationController : Controller
    {
        private readonly ILimitationService _limitationService;

        public LimitationController(ILimitationService limitationService)
        {
            _limitationService = limitationService;
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, int suggestionId)
        {
            string? limitationValue = collection["Limitation"];

            if (limitationValue == null)
            {
                return View("Index", "Suggestion");
            }

            Limitation limitation = new()
            {
                Name = limitationValue,
            };

            limitation = await _limitationService.CreateLimitationAsync(limitation);
            await _limitationService.CreateActivityLimitationAsync(limitation, suggestionId);

            return RedirectToAction("Details", "Suggestion", new { suggestionId = suggestionId });
        }
    }
}
