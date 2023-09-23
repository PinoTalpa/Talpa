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
        Task<Suggestion> CreateSuggestionAsync(Suggestion suggestion);
        Task<Suggestion> GetSuggestionByIdAsync(int id);
    }
}
