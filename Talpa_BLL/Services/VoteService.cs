using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa_BLL.Services
{
    public class VoteService : IVoteService
    {
        public Task<Vote> CreateVoteAsync(Vote vote)
        {
            throw new NotImplementedException();
        }
    }
}
