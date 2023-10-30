using ModelLayer.Enums;
using ModelLayer.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Interfaces;

namespace Talpa_BLL.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository) 
        {
            _activityRepository = activityRepository;
        }

        //public async Task<List<Activity>> GetActivitiesAsync(string searchString)
        //{
        //    List<ActivityDto> activityDtos = await _activityRepository.GetActivitiesAsync(searchString);

        //    List<Activity> activities = activityDtos.Select(activity => new Activity
        //    {
        //        Id = activity.Id,
        //        Name = activity.Name,
        //        Description = activity.Description,
        //        ImageUrl = activity.ImageUrl,
        //        Date = (DateTime?)activity.Date,
        //        ActivityState = (Talpa_DAL.Enums.ActivityState)activity.ActivityState,
        //    }).ToList();

        //    return activities;
        //}

        public async Task<List<Activity>> GetActivitiesWithSuggestionsAsync()
        {
            List<ActivityDateDto> activityDtos = await _activityRepository.GetActivitiesWithSuggestionsAsync();

            // Group SuggestionDto objects by startTime and endTime
            var groupedSuggestions = activityDtos.GroupBy(dto => new { dto.StartDate, dto.EndDate });

            // Create Activity objects based on the grouped suggestions
            List<Activity> activities = groupedSuggestions.Select(group =>
            {
                var firstDto = group.First(); // Take the first element for startTime and endTime
                var activity = new Activity
                {
                    Id = firstDto.Id,
                    startTime = firstDto.StartDate,
                    endTime = firstDto.EndDate,
                    Suggestions = group.Select(dto => new Suggestion
                    {
                        Id = dto.Suggestion.Id,
                        Name = dto.Suggestion.Name,
                        Description = dto.Suggestion.Description,
                        ImageUrl = dto.Suggestion.ImageUrl,
                        Date = (DateTime?)dto.Suggestion.Date,
                        ActivityState = (Talpa_DAL.Enums.ActivityState)dto.Suggestion.ActivityState
                    }).ToList()
                };
                return activity;
            }).ToList();

            return activities;
        }


        //public async Task<Activity> GetActivityByIdAsync(int id)
        //{
        //    if (id <= 0)
        //    { 
        //        return new Activity { ErrorMessage = "Invalid activity." };
        //    }

        //    SuggestionDto? activityDto = await _activityRepository.GetActivityByIdAsync(id);

        //    if (activityDto == null)
        //    {
        //        return new Activity { ErrorMessage = "Activity not found." };
        //    }

        //    Activity activity = new()
        //    {
        //        Id = activityDto.Id,
        //        UserId = activityDto.UserId,
        //        Name = activityDto.Name,
        //        Description = activityDto.Description,
        //        ImageUrl = activityDto.ImageUrl,
        //        Date = (DateTime?)activityDto.Date,
        //        ActivityState = (Talpa_DAL.Enums.ActivityState)activityDto.ActivityState,
        //    };

        //    return activity;
        //}

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

        //public async Task<Activity> RemoveActivityAsync(Activity activity)
        //{
        //    ActivityDto activityDto = new()
        //    {
        //        Id = activity.Id,
        //        UserId = activity.UserId,
        //        Name = activity.Name,
        //        Description = activity.Description,
        //        ImageUrl = activity.ImageUrl,
        //        Date = (DateTime?)activity.Date,
        //        ActivityState = ActivityState.Rejected,
        //    };

        //    bool isActivityDeclined = await _activityRepository.RemoveActivityAsync(activityDto);

        //    if (!isActivityDeclined)
        //    {
        //        activity.ErrorMessage = "There was an error while removing the activity!";
        //    }

        //    return activity;
        //}

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
