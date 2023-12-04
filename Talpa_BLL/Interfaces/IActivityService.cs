using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface IActivityService
    {
        Task<List<Suggestion>> GetActivitiesAsync(string searchString);
        Task<List<Activity>> GetActivitiesWithSuggestionsAsync();
        // Task<Activity> GetActivityByIdAsync(int id);
        Task<Suggestion> CreateActivityAsync(Suggestion suggestion);
        // Task<Activity> RemoveActivityAsync(Activity activity);

        Task<List<ActivityDate>> GetActivityDates(int activityId);
    }
}
