using ModelLayer.Models;

namespace Talpa_DAL.Interfaces
{
    public interface IVoteRepository
    {
        Task<bool> CreateVote(VoteDto vote);
        Task<List<VoteDto>> GetAllVotesBySuggestionId(int Id);
        int GetVoteCountBySuggestionAsync(int Id);
        Task<VoteDto> GetVoteBySuggestionId(VoteDto vote);
    }
}
