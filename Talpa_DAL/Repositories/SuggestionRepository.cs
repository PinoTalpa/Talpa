﻿using Microsoft.EntityFrameworkCore;
using ModelLayer.Enums;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Talpa_BLL.Models;
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

        public async Task<List<SuggestionDto>> GetPendingSuggestionsAsync(string searchString)
        {
            IQueryable<SuggestionDto> query = _dbContext.Suggestions.Select(s => new SuggestionDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Date = s.Date,
                ImageUrl = s.ImageUrl,
                ActivityState = s.ActivityState,
            });

            query = query.Where(s => s.ActivityState.Equals(ActivityState.Pending));

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<List<SuggestionDto>> GetSuggestionsAsync(string searchString)
        {
            IQueryable<SuggestionDto> query = _dbContext.Suggestions.Select(s => new SuggestionDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Date = s.Date,
                ImageUrl = s.ImageUrl,
                ActivityState = s.ActivityState,
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<ChosenSuggestion?> GetChosenSuggestionByIdAsync(int id)
        {
            ChosenSuggestion? suggestionDto = await _dbContext.ChosenSuggestions.FindAsync(id);

            return suggestionDto;
        }

        public async Task<List<LeaderboardDto>> GetExecutedSuggestionsAsync()
        {
            IQueryable<LeaderboardDto> query = from user in _dbContext.Users
                                               join chosenSuggestion in _dbContext.ChosenSuggestions on user.Id equals chosenSuggestion.UserId
                                               where chosenSuggestion.Date != null
                                               group user by user.Name into userGroup
                                               select new LeaderboardDto
                                               {
                                                   UserName = userGroup.Key,
                                                   ExecutedSuggestionCount = userGroup.Count()
                                               };

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

        public async Task<bool> DeclineSuggestionAsync(SuggestionDto suggestion)
        {
            try
            {
                var existingSuggestion = await _dbContext.Suggestions.FindAsync(suggestion.Id);

                if (existingSuggestion != null)
                {
                    _dbContext.Suggestions.Remove(existingSuggestion);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SuggestionNameExistsAsync(string suggestionName)
        {
            return await _dbContext.Suggestions
                .AnyAsync(s => string.Equals(s.Name, suggestionName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
