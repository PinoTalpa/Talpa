using ModelLayer.Enums;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Interfaces;
using Talpa_DAL.Repositories;

namespace Talpa_BLL.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository) 
        {
            _activityRepository = activityRepository;
        }

        public async Task<List<Activity>> GetActivitiesAsync(string searchString)
        {
            List<ActivityDto> activityDtos = await _activityRepository.GetActivitiesAsync(searchString);

            List<Activity> activities = activityDtos.Select(activity => new Activity
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ImageUrl = activity.ImageUrl,
                Date = (DateTime?)activity.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)activity.ActivityState,
            }).ToList();

            return activities;
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            if (id <= 0)
            { 
                return new Activity { ErrorMessage = "Invalid activity." };
            }

            SuggestionDto? activityDto = await _activityRepository.GetActivityByIdAsync(id);

            if (activityDto == null)
            {
                return new Activity { ErrorMessage = "Activity not found." };
            }

            Activity activity = new()
            {
                Id = activityDto.Id,
                UserId = activityDto.UserId,
                Name = activityDto.Name,
                Description = activityDto.Description,
                ImageUrl = activityDto.ImageUrl,
                Date = (DateTime?)activityDto.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)activityDto.ActivityState,
            };

            return activity;
        }

        public async Task<Suggestion> CreateActivityAsync(Suggestion suggestion)
        {
            SuggestionDto suggestionDto = new()
            {
                Id = suggestion.Id,
                UserId = suggestion.UserId,
                Name = suggestion.Name,
                Description = suggestion.Description,
                ImageUrl = suggestion.ImageUrl,
                Date = (DateTime?)suggestion.Date,
                ActivityState = ActivityState.Accepted,
            };

            bool isSuggestionDeclined = await _activityRepository.CreateActivityAsync(suggestionDto);

            if (!isSuggestionDeclined)
            {
                suggestion.ErrorMessage = "There was an error while declining the suggestion!";
            }

            return suggestion;
        }

        public async Task<Activity> RemoveActivityAsync(Activity activity)
        {
            ActivityDto activityDto = new()
            {
                Id = activity.Id,
                UserId = activity.UserId,
                Name = activity.Name,
                Description = activity.Description,
                ImageUrl = activity.ImageUrl,
                Date = (DateTime?)activity.Date,
                ActivityState = ActivityState.Rejected,
            };

            bool isActivityDeclined = await _activityRepository.RemoveActivityAsync(activityDto);

            if (!isActivityDeclined)
            {
                activity.ErrorMessage = "There was an error while removing the activity!";
            }

            return activity;
        }

        public async Task<List<ActivityDate>> GetActivityDates(int activityId)
        {
            List<ActivityDateDto> activityDateDto = await _activityRepository.GetActivityDates(activityId);

            List<ActivityDate> activitieDates = activityDateDto.Select(activityDate => new ActivityDate
            {
                Id = (int)activityDate.Id,
                SuggestionId = activityDate.SuggestionId,
                StartDate = activityDate.StartDate,
                EndDate = activityDate.EndDate,
            }).ToList();

            return activitieDates;
        }
    }
}
