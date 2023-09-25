using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Activity>> GetActivitiesAsync(string searchString)
        {
            List<ActivityDto> activityDtos = await _activityRepository.GetActivitiesAsync(searchString);

            List<Activity> activities = activityDtos.Select(activity => new Activity
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Date = (DateTime?)activity.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)activity.ActivityState,
            }).ToList();

            return activities;
        }
    }
}
