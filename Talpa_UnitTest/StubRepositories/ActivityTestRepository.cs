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
        List<SuggestionDto> activities;
        List<ActivityDateDto> activityDates;

        public void InitializeActivities(List<SuggestionDto> activityDtos)
        {
            activities = activityDtos;
        }

        public void InitializeActivitiesDates(List<ActivityDateDto> activityDatesDtos)
        {
            activityDates = activityDatesDtos;
        }

        public async Task<List<SuggestionDto>> GetActivitiesAsync(string searchString)
        {
            return activities;
        }

        public async Task<List<ActivityDateDto>> GetActivitiesWithSuggestionsAsync()
        {
            return activityDates;
        }

        public async Task<SuggestionDto?> GetActivityByIdAsync(int id)
        {
            SuggestionDto activity = new();

            foreach (var item in activities)
            {
                if (item.Id == id)
                {
                    activity = item;
                    break;
                }
            }

            return activity;
        }

        public async Task<bool> CreateActivityAsync(SuggestionDto activity)
        {
            foreach (var item in activities)
            {
                if (activity.Id == item.Id)
                {
                    return false;
                }
            }

            activities.Add(activity);
            return true;
        }

        public async Task<List<ActivityDateDto>> GetActivityDates(int activityId)
        {
            var result = activityDates
            .Where(activityDate => activityDate.SuggestionId == activityId)
            .ToList();

            return result;
        }
    }
}
