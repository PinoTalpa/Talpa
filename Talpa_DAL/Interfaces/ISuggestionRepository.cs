using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Entities;

namespace Talpa_DAL.Interfaces
{
    public interface ISuggestionRepository
    {
        Task<List<SuggestionDto>> GetSuggestionsAsync(string searchString);
        Task<bool> CreateSuggestionAsync(SuggestionDto suggestion);
        Task<SuggestionDto?> GetSuggestionByIdAsync(int id);
        Task<bool> DeclineSuggestionAsync(SuggestionDto suggestion);
    }
}
