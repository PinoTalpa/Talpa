using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Suggestion>> GetSuggestionsAsync(string searchString)
        {
            List<SuggestionDto> suggestionDtos = await _suggestionRepository.GetSuggestionsAsync(searchString);

            List<Suggestion> suggestions = suggestionDtos.Select(suggestion => new Suggestion
            {
                Id = suggestion.Id,
                Name = suggestion.Name,
                Description = suggestion.Description,
                Date = suggestion.Date,
                ActivityState = (Talpa_DAL.Enums.ActivityState)suggestion.ActivityState,
            }).ToList();

            return suggestions;
        }
    }
}
