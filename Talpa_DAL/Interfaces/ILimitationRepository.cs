using ModelLayer.Models;
using Talpa_DAL.Entities;

namespace Talpa_DAL.Interfaces
{
    public interface ILimitationRepository
    {
        Task<List<LimitationDto>> GetLimitationsBySuggestionId(int suggestionId);
        Task<LimitationDto> CreateLimitationAsync(LimitationDto limitationDto);
        Task<bool> CreateActivityLimitationAsync(ActivityLimitationDto activityLimitationDto);
    }
}
