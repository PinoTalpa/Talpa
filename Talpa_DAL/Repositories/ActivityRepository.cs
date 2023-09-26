using Microsoft.EntityFrameworkCore;
using ModelLayer.Enums;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Data;
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

        public async Task<List<ActivityDto>> GetActivitiesAsync(string searchString)
        {
            IQueryable<ActivityDto> query = _dbContext.Suggestions.Select(s => new ActivityDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Date = s.Date,
                ActivityState = s.ActivityState,
            });

            query = query.Where(s => s.ActivityState.Equals(ActivityState.Accepted));

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<SuggestionDto?> GetActivityByIdAsync(int id)
        {
            SuggestionDto? activityDto = await _dbContext.Suggestions.FindAsync(id);

            return activityDto;
        }
    }
}
