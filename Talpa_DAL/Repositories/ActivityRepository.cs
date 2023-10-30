using Microsoft.EntityFrameworkCore;
using ModelLayer.Enums;
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
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ActivityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<List<ActivityDto>> GetActivitiesAsync(string searchString)
        //{
        //    IQueryable<ActivityDto> query = _dbContext.Suggestions.Select(s => new ActivityDto
        //    {
        //        Id = s.Id,
        //        Name = s.Name,
        //        Description = s.Description,
        //        ImageUrl = s.ImageUrl,
        //        Date = s.Date,
        //        ActivityState = s.ActivityState,
        //    });

        //    query = query.Where(s => s.ActivityState.Equals(ActivityState.Accepted));

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        query = query.Where(s => s.Name.Contains(searchString));
        //    }

        //    return await query.ToListAsync();
        //}

        public async Task<List<ActivityDateDto>> GetActivitiesWithSuggestionsAsync()
        {
            var query = _dbContext.Suggestions
                .Join(
                    _dbContext.ActivityDates,
                    suggestion => suggestion.Id,
                    activity => activity.SuggestionId,
                    (suggestion, activity) => new ActivityDateDto
                    {
                        Id = suggestion.Id,
                        StartDate = activity.StartDate.Date,
                        EndDate = activity.EndDate.Date,
                        SuggestionId = suggestion.Id,
                        Suggestion = new SuggestionDto
                        {
                            Id = suggestion.Id,
                            Name = suggestion.Name,
                            Description = suggestion.Description,
                            Date = suggestion.Date,
                            ActivityState = suggestion.ActivityState,
                            ImageUrl = suggestion.ImageUrl
                        }
                    })
                .Where(result => result.Suggestion.Date == null && result.Suggestion.ActivityState == ActivityState.Accepted)
                .ToList();

            return query;
        }



        public async Task<List<ActivityDateDto>> GetActivityDates(int activityId)
        {
            List<ActivityDateDto> activityDates = _dbContext.ActivityDates
                .Where(date => date.SuggestionId == activityId)
                .ToList();

            return activityDates;
        }

        // Move this to the suggestion part of the application/or rework.

        public async Task<bool> CreateActivityAsync(SuggestionDto suggestion)
        {
            try
            {
                var existingSuggestion = await _dbContext.Suggestions.FindAsync(suggestion.Id);

                if (existingSuggestion != null)
                {
                    existingSuggestion.ActivityState = suggestion.ActivityState;
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        // Move this to the suggestion part of the application/or rework.

        public async Task<SuggestionDto?> GetActivityByIdAsync(int id)
        {
            SuggestionDto? activityDto = await _dbContext.Suggestions.FindAsync(id);

            return activityDto;
        }

        // Move this to the suggestion part of the application.

        //public async Task<bool> RemoveActivityAsync(ActivityDto activity)
        //{
        //    try
        //    {
        //        var existingActivity = await _dbContext.Suggestions.FindAsync(activity.Id);

        //        if (existingActivity != null)
        //        {
        //            existingActivity.ActivityState = activity.ActivityState;
        //            await _dbContext.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
