using ModelLayer.Models;
using Talpa_BLL.Models;

namespace Talpa_DAL.Interfaces
{
    public interface ISuggestionRepository
    {
        Task<List<SuggestionDto>> GetSuggestionsAsync(string searchString);
        Task<List<SuggestionDto>> GetPendingSuggestionsAsync(string searchString);
        Task<List<LeaderboardDto>> GetExecutedSuggestionsAsync();
        Task<bool> CreateSuggestionAsync(SuggestionDto suggestion);
        Task<SuggestionDto?> GetSuggestionByIdAsync(int id);
        Task<bool> DeclineSuggestionAsync(SuggestionDto suggestion);
        Task<bool> SuggestionNameExistsAsync(string suggestionName);
    }
}
