using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface IVoteService
    {
        Task<Vote> CreateVoteAsync(Vote vote);
        Task<Vote> getExistingVoteAsync(Vote vote);
        Task<List<Vote>> GetAllVotesBySuggestionAsync(int id);
        int GetVoteCountBySuggestionAsync(int Id);
        Task<bool> DeleteExistingVoteAsync(Vote vote);
    }
}
