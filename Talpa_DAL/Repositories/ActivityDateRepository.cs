using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Data;
using Talpa_DAL.Entities;
using Talpa_DAL.Interfaces;

namespace Talpa_DAL.Repositories
{
    public class ActivityDateRepository : IActivityDateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ActivityDateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ActivityDateDto>> GetActivitiesDateWithId(int selectedSuggestionId)
        {
            var query = from userActivityDate in _dbContext.UserActivityDates
                        join activityDate in _dbContext.ActivityDates
                        on userActivityDate.ActivityDateId equals activityDate.Id
                        where userActivityDate.IsAvailable // You can add other conditions here if needed
                        && activityDate.SuggestionId == selectedSuggestionId
                        select new ActivityDateDto
                        {
                            Id = activityDate.Id,
                            StartDate = activityDate.StartDate,
                            EndDate = activityDate.EndDate,
                            SuggestionId = activityDate.SuggestionId
                            // Add other properties as needed
                        };

            var result = await query.ToListAsync(); // Assuming you're using async operations

            return result;
        }

        //public async Task<<List<ActionResult> SaveChosenActivity(int SuggestionId, int OtherSuggestionId1, int OtherSuggestionId2, DateTime Date)
        //{
        //    // Assuming you have a DbContext named YourDbContext
        //    using (var dbContext = new _dbContext.Suggestions())
        //    {
        //        // Find the suggestion by Id
        //        var suggestion = await dbContext.Suggestions.FindAsync(SuggestionId);

        //        if (suggestion != null)
        //        {
        //            // Create a new ChosenSuggestion based on the Suggestion
        //            var chosenSuggestion = new ChosenSuggestion
        //            {
        //                SuggestionId = suggestion.Id,
        //                // Copy other properties as needed...
        //                Date = Date,
        //                // Set OtherSuggestionId1 and OtherSuggestionId2 as needed...
        //            };

        //            // Add the new ChosenSuggestion to the ChosenSuggestions table
        //            dbContext.ChosenSuggestions.Add(chosenSuggestion);

        //            // Save changes to the database
        //            await dbContext.SaveChangesAsync();
        //        }
        //    }

           
        //}


        public async Task CreateActivityDates(List<ActivityDateDto> activityDates)
        {
            try
            {
                foreach (ActivityDateDto activityDate in activityDates)
                {
                    _dbContext.ActivityDates.Add(activityDate);
                }

                _dbContext.SaveChanges();
            }
            catch
            {
                // throw exception or som
            }
        }
    }
}
