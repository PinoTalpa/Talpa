using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Interfaces
{
    public interface IActivityRepository
    {
        Task<List<ActivityDto>> GetActivitiesAsync(string searchString);
        Task<SuggestionDto?> GetActivityByIdAsync(int id);
    }
}
