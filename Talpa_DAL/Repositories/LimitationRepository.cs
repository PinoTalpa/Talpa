using Microsoft.EntityFrameworkCore;
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
    public class LimitationRepository : ILimitationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LimitationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LimitationDto>> GetLimitationsBySuggestionId(int suggestionId)
        {
            var limitations = await _dbContext.ActivityLimitations
                .Where(al => al.SuggestionId == suggestionId)
                .Select(al => al.Limitation)
                .ToListAsync();

            return limitations;
        }

        public async Task<LimitationDto> CreateLimitationAsync(LimitationDto limitationDto)
        {
            try
            {
                _dbContext.Limitations.Add(limitationDto);
                await _dbContext.SaveChangesAsync();
                return limitationDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateActivityLimitationAsync(ActivityLimitationDto activityLimitationDto)
        {
            try
            {
                _dbContext.ActivityLimitations.Add(activityLimitationDto);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
