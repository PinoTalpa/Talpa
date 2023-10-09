using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Security.Claims;
using System.Web;
using Talpa.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa.Controllers
{
    public class VoteController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IVoteService _voteService;

        public VoteController(IActivityService activityService, IVoteService voteService)
        {
            _activityService = activityService;
            _voteService = voteService;
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
            return RedirectToAction(nameof(Index));
        }

        // POST: VoteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(List<DateTime> selectedDates, int suggestionId)
        {
            //int.TryParse(collection["activityId"], out int ActivityId);
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (selectedDates.Count == 0 || suggestionId == null)
            {
                return RedirectToAction("Index", "Activity");
            }

            Vote vote = new()
            {
                UserId = userId,
                SuggestionId = suggestionId,
                SelectedDates = selectedDates
            };

            //TODO vote creation with interface
            vote = await _voteService.CreateVoteAsync(vote);

            return RedirectToAction(nameof(Index), "Activity");
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
