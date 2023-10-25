using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Entities;

namespace Talpa_DAL.Interfaces
{
    public interface IVoteRepository
    {
        Task<bool> CreateVote(VoteDto vote);
    }
}
