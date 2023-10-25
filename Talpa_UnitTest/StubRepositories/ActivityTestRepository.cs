using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class ActivityTestRepository : IActivityRepository
    {
        List<ActivityDto> activities;
        List<SuggestionDto> suggestions;
        List<ActivityDateDto> activityDates;

        public void InitializeActivities(List<ActivityDto> activityDtos)
        {
            activities = activityDtos;
        }

        public void InitializeActivityDates(List<ActivityDateDto> activityDatesDtos)
        {
            activityDates = activityDatesDtos;
        }

        public void InitializeSuggestions(List<SuggestionDto> suggestionDtos)
        {
            suggestions = suggestionDtos;
        }

        public async Task<List<ActivityDto>> GetActivitiesAsync(string searchString)
        {
            return activities;
        }

        public async Task<List<ActivityDateDto>> GetActivityDates(int activityId)
        {
            return activityDates.Where(date => date.SuggestionId == activityId).ToList();
        }

        public async Task<bool> CreateActivityAsync(SuggestionDto suggestion)
        {
            foreach (var item in suggestions)
            {
                if (suggestion.Id == item.Id)
                {
                    item.ActivityState = suggestion.ActivityState;
                    return true;
                }
            }

            return false;
        }

        public async Task<SuggestionDto?> GetActivityByIdAsync(int id)
        {
            SuggestionDto suggestion = new();

            foreach (var item in suggestions)
            {
                if (item.Id == id)
                {
                    suggestion = item;
                    break;
                }
            }

            return suggestion;
        }

        public async Task<bool> RemoveActivityAsync(ActivityDto activity)
        {
            foreach (var item in activities)
            {
                if (activity.Id == item.Id)
                {
                    item.ActivityState = activity.ActivityState;
                    return true;
                }
            }

            return false;
        }
    }
}
