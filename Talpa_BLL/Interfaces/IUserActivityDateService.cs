using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_BLL.Interfaces
{
    public interface IUserActivityDateService
    {
        Task<bool> AddUserActivityDateAsync(string userId, int ActivityDateId, bool IsAvaliable = true);
    }
}
