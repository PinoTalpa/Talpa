using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface ISuggestionService
    {
        Task<List<Suggestion>> GetSuggestionsAsync(string searchString);
        Task<List<Suggestion>> GetPendingSuggestionsAsync(string searchString);
        Task<List<Leaderboard>> GetExecutedSuggestionsAsync();
        Task<Suggestion> CreateSuggestionAsync(Suggestion suggestion);
        Task<Suggestion> GetSuggestionByIdAsync(int id);
        Task<Suggestion?> GetChosenSuggestionByIdAsync(int id);
        Task<Suggestion> DeclineSuggestionAsync(Suggestion suggestion);
        Task<bool> SuggestionNameExistsAsync(string suggestionName);
    }
}
