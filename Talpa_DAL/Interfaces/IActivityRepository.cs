using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Entities;

namespace Talpa_DAL.Interfaces
{
    public interface IActivityRepository
    {
        Task<List<ActivityDto>> GetActivitiesAsync(string searchString);
        Task<SuggestionDto?> GetActivityByIdAsync(int id);
        Task<bool> CreateActivityAsync(SuggestionDto suggestion);
        Task<bool> RemoveActivityAsync(ActivityDto activity);
        Task<List<ActivityDateDto>> GetActivityDates(int activityId);
    }
}
