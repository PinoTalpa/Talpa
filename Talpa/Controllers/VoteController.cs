using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Controllers
{
    public class VoteController : Controller
    {
        private readonly IActivityService _activityService;

        public VoteController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<ActionResult> Index(int activityId)
        {
            List<ActivityDate> activityDates = await _activityService.GetActivityDates(activityId);

            List<ActivityDateViewModel> activitieDates = activityDates.Select(activityDate => new ActivityDateViewModel
            {
                Id = (int)activityDate.Id,
                SuggestionId = activityDate.SuggestionId,
                StartDate = activityDate.StartDate,
                EndDate = activityDate.EndDate,
            }).ToList();

            return View(activitieDates);
        }

        // GET: VoteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VoteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: VoteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VoteController/Edit/5
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

        // GET: VoteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VoteController/Delete/5
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
    }
}
