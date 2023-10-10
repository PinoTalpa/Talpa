using Microsoft.EntityFrameworkCore;
using ModelLayer.Enums;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Data;
using Talpa_DAL.Entities;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class SuggestionTestRepository : ISuggestionRepository
    {
        List<SuggestionDto> suggestions;

        public void InitializeSuggestions(List<SuggestionDto> suggestionDtos)
        {
            suggestions = suggestionDtos;
        }

        public async Task<List<SuggestionDto>> GetPendingSuggestionsAsync(string searchString)
        {
            if (suggestions == null)
            {
                throw new InvalidOperationException("Suggestions list has not been initialized.");
            }

            var filteredSuggestions = suggestions
                .Where(s => s.ActivityState == ActivityState.Pending);

            if (!string.IsNullOrEmpty(searchString))
            {
                filteredSuggestions = filteredSuggestions
                    .Where(s => s.Name.Contains(searchString));
            }

            return filteredSuggestions.ToList();
        }

        public async Task<List<SuggestionDto>> GetSuggestionsAsync(string searchString)
        {
            return suggestions;
        }

        public async Task<SuggestionDto?> GetSuggestionByIdAsync(int id)
        {
            SuggestionDto suggestion = new();

            foreach (var item in suggestions)
            {
                if (item.Id == id)
                {
                    suggestion = item;
                    break;
                }
            }

            return suggestion;
        }

        public async Task<bool> CreateSuggestionAsync(SuggestionDto suggestion)
        {
            foreach (var item in suggestions)
            {
                if (suggestion.Id == item.Id)
                {
                    return false;
                }
            }

            suggestions.Add(suggestion);
            return true;
        }

        public async Task<bool> DeclineSuggestionAsync(SuggestionDto suggestion)
        {
            var existingSuggestion = suggestions.FirstOrDefault(s => s.Id == suggestion.Id);

            if (existingSuggestion != null)
            {
                existingSuggestion.ActivityState = suggestion.ActivityState;
                return true;
            }

            return false;
        }
    }
}
