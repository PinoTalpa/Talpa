using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface ILimitationService
    {
        Task<List<Limitation>> GetLimitationsBySuggestionId(int suggestionId);
        Task<Limitation> CreateLimitationAsync(Limitation limitation);

        Task<bool> CreateActivityLimitationAsync(Limitation limitation, int suggestionId);
    }
}
