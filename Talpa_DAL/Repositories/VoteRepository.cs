using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Data;
using Talpa_DAL.Entities;
using Talpa_DAL.Interfaces;

namespace Talpa_DAL.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateVote(VoteDto vote)
        {
            try
            {
                _dbContext.Votes.Add(vote);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<List<VoteDto>> GetAllVotesBySuggestionId(int Id)
        {
            List<VoteDto> foundVotes = _dbContext.Votes
                .Where(v => v.SuggestionId == Id)
                .ToList();

            return foundVotes;
        }

        public Task<Vote> getVote(VoteDto vote)
        {
            throw new NotImplementedException();
        }

        public async Task<VoteDto> GetVoteBySuggestionId(VoteDto vote)
        {
            VoteDto foundVote = _dbContext.Votes
                .FirstOrDefault(v => v.UserId == vote.UserId && v.SuggestionId == vote.SuggestionId);

            return foundVote;
        }

        Task<VoteDto> IVoteRepository.getVote(VoteDto vote)
        {
            throw new NotImplementedException();
        }
    }
}
