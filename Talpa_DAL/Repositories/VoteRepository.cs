using Microsoft.EntityFrameworkCore;
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
    }
}
