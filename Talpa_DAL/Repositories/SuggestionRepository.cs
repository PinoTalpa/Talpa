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
    public class SuggestionRepository : ISuggestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SuggestionDto>> GetSuggestionsAsync(string searchString)
        {
            IQueryable<SuggestionDto> query = _dbContext.Suggestions.Select(s => new SuggestionDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Date = s.Date,
                ActivityState = s.ActivityState,
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<SuggestionDto?> GetSuggestionByIdAsync(int id)
        {
            SuggestionDto? suggestionDto = await _dbContext.Suggestions.FindAsync(id);

            return suggestionDto;
        }

        public async Task<bool> CreateSuggestionAsync(SuggestionDto suggestion)
        {
            try
            {
                _dbContext.Suggestions.Add(suggestion);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
