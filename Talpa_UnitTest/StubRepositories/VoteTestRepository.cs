using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class VoteTestRepository : IVoteRepository
    {
        private List<VoteDto> votes = new();

        public void InitializeVotes(List<VoteDto> voteDtos)
        {
            votes = voteDtos;
        }

        public async Task<bool> CreateVote(VoteDto vote)
        {
            foreach (var item in votes)
            {
                if (vote.Id == item.Id)
                {
                    return false;
                }
            }

            votes.Add(vote);
            return true;
        }

        public Task<List<VoteDto>> GetAllVotesBySuggestionId(int Id)
        {
            var foundVotes = votes
                .Where(v => v.SuggestionId == Id)
                .ToList();

            return Task.FromResult(foundVotes);
        }

        public int GetVoteCountBySuggestionAsync(int Id)
        {
            var foundVotes = votes
                .Count(v => v.SuggestionId == Id);

            return foundVotes;
        }

        public Task<VoteDto> GetVoteBySuggestionId(VoteDto vote)
        {
            var foundVote = votes
                .FirstOrDefault(v => v.UserId == vote.UserId && v.SuggestionId == vote.SuggestionId);

            return Task.FromResult(foundVote);
        }
    }
}
