using ModelLayer.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Entities;
using Talpa_DAL.Interfaces;
using Vote = Talpa_BLL.Models.Vote;

namespace Talpa_BLL.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }
        public async Task<Vote> CreateVoteAsync(Vote vote)
        {
            VoteDto voteDto = new()
            {
                UserId = vote.UserId,
                SuggestionId = vote.SuggestionId,
            };

            bool isVoteCreated = await _voteRepository.CreateVote(voteDto);

            vote = new Vote
            {
                UserId = voteDto.UserId,
                SuggestionId = voteDto.SuggestionId,
            };

            if (!isVoteCreated)
            {
                vote.ErrorMessage = "There was an error while creating the vote!";
            }

            return vote;
        }

        public Task<bool> DeleteExistingVoteAsync(Vote vote)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Vote>> GetAllVotesBySuggestionAsync(int Id)
        {
            List<VoteDto> votes = await _voteRepository.GetAllVotesBySuggestionId(Id);
            List<Vote> results = new List<Vote>();

            foreach(VoteDto vote in votes)
            {
                Vote result = new()
                {
                    Id = vote.Id,
                    UserId = vote.UserId,
                    SuggestionId = vote.SuggestionId,
                };
                results.Add(result);
            }
            return results;
        }

        public async Task<Vote> getExistingVoteAsync(Vote vote)
        {
            VoteDto voteDto = new()
            {
                UserId = vote.UserId,
                SuggestionId = vote.SuggestionId,
            };

            VoteDto isExistingVote = await _voteRepository.GetVoteBySuggestionId(voteDto);
            
            if(isExistingVote == null)
            {
                return new Vote();
            }
            else
            {
                Vote found = new()
                {
                    Id = isExistingVote.Id,
                    UserId = isExistingVote.UserId,
                    SuggestionId = isExistingVote.SuggestionId,
                };
                return found;
            }
        }
    }
}
