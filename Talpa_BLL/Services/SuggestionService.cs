using ModelLayer.Enums;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Interfaces;

namespace Talpa_BLL.Services
{
    public class SuggestionService : ISuggestionService
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionService(ISuggestionRepository suggestionRepository) 
        {
            _suggestionRepository = suggestionRepository;
        }

        public async Task<List<Suggestion>> GetPendingSuggestionsAsync(string searchString)
        {
            List<SuggestionDto> suggestionDtos = await _suggestionRepository.GetPendingSuggestionsAsync(searchString);

            List<Suggestion> suggestions = suggestionDtos.Select(suggestion => new Suggestion
            {
                Id = suggestion.Id,
                Name = suggestion.Name,
                Description = suggestion.Description,
                ImageUrl = suggestion.ImageUrl,
                Date = (DateTime?)suggestion.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
            }).ToList();

            return suggestions;
        }

        public async Task<List<Suggestion>> GetSuggestionsAsync(string searchString)
        {
            List<SuggestionDto> suggestionDtos = await _suggestionRepository.GetSuggestionsAsync(searchString);

            List<Suggestion> suggestions = suggestionDtos.Select(suggestion => new Suggestion
            {
                Id = suggestion.Id,
                Name = suggestion.Name,
                Description = suggestion.Description,
                ImageUrl = suggestion.ImageUrl,
                Date = (DateTime?)suggestion.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
            }).ToList();

            return suggestions;
        }

        public async Task<List<ActivityDate>> ConvertActivityDateDtos(List<ActivityDateDto> activityDateDtos)
        {
            List<ActivityDate> activityDates = activityDateDtos.Select(activityDto => new ActivityDate
            {
                Id = activityDto.Id,
                StartDate = activityDto.StartDate,
                EndDate = activityDto.EndDate,
                SuggestionId = activityDto.SuggestionId
                // Add other properties as needed
            }).ToList();

            return activityDates;
        }


        public async Task<List<Leaderboard>> GetExecutedSuggestionsAsync()
        {
            List<LeaderboardDto> leaderboardDtos = await _suggestionRepository.GetExecutedSuggestionsAsync();

            List<Leaderboard> leaderboards = leaderboardDtos.Select(leaderboard => new Leaderboard
            {
                UserName = leaderboard.UserName,
                ExecutedSuggestionCount = leaderboard.ExecutedSuggestionCount,
            }).ToList();

            return leaderboards;
        }

        public async Task<Suggestion> GetSuggestionByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Suggestion { ErrorMessage = "Invalid suggestion." };
            }

            SuggestionDto? suggestionDto = await _suggestionRepository.GetSuggestionByIdAsync(id);

            if (suggestionDto == null)
            {
                return new Suggestion { ErrorMessage = "Suggestion not found." };
            }

            Suggestion suggestion = new()
            {
                Id = suggestionDto.Id, 
                UserId = suggestionDto.UserId,
                Name = suggestionDto.Name, 
                Description = suggestionDto.Description,
                ImageUrl = suggestionDto.ImageUrl,
                Date = (DateTime?)suggestionDto.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestionDto.ActivityState,
            };

            return suggestion;
        }

        public async Task<Suggestion> GetChosenSuggestionByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new Suggestion { ErrorMessage = "Invalid suggestion." };
            }

            ChosenSuggestion? suggestionDto = await _suggestionRepository.GetChosenSuggestionByIdAsync(id);

            if (suggestionDto == null)
            {
                return new Suggestion { ErrorMessage = "Suggestion not found." };
            }

            Suggestion suggestion = new()
            {
                Id = suggestionDto.Id,
                UserId = suggestionDto.UserId,
                Name = suggestionDto.Name,
                Description = suggestionDto.Description,
                ImageUrl = suggestionDto.ImageUrl,
                Date = (DateTime?)suggestionDto.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestionDto.ActivityState,
            };

            return suggestion;
        }

        public async Task<Suggestion> CreateSuggestionAsync(Suggestion suggestion)
        {
            if (string.IsNullOrEmpty(suggestion.Name) || string.IsNullOrEmpty(suggestion.Description))
            {
                suggestion.ErrorMessage = "Suggestion name or description has an incorrect input.";
                return suggestion;
            }

            SuggestionDto suggestionDto = new()
            {
                UserId = suggestion.UserId,
                Name = suggestion.Name,
                Description = suggestion.Description,
                ImageUrl = suggestion.ImageUrl,
                ActivityState = ModelLayer.Enums.ActivityState.Pending,
            };

            bool isSuggestionCreated = await _suggestionRepository.CreateSuggestionAsync(suggestionDto);

            suggestion = new Suggestion
            {
                UserId = suggestionDto.UserId,
                Name = suggestionDto.Name,
                Description = suggestionDto.Description,
                ImageUrl = suggestionDto.ImageUrl,
                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestionDto.ActivityState
            };

            if (!isSuggestionCreated)
            {
                suggestion.ErrorMessage = "There was an error while creating the suggestion!";
            }

            return suggestion;
        }

        public async Task<Suggestion> DeclineSuggestionAsync(Suggestion suggestion)
        {
            SuggestionDto suggestionDto = new()
            {
                Id = suggestion.Id,
                UserId = suggestion.UserId,
                Name = suggestion.Name,
                Description = suggestion.Description,
                ImageUrl = suggestion.ImageUrl,
                Date = (DateTime?)suggestion.Date,
                ActivityState = ActivityState.Rejected,
            };

            bool isSuggestionDeclined = await _suggestionRepository.DeclineSuggestionAsync(suggestionDto);

            if (!isSuggestionDeclined)
            {
                suggestion.ErrorMessage = "There was an error while declining the suggestion!";
            }

            return suggestion;
        }

        public async Task<bool> SuggestionNameExistsAsync(string suggestionName)
        {
            bool exists = await _suggestionRepository.SuggestionNameExistsAsync(suggestionName);

            return exists;
        }
    }
}
