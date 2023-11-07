using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface IUserService
    {
        Task Login(string userId, string name, string email);
        Task<User> UpdateUserAsync(User user);
        Task<UserDto> GetUserAsync(string userId);

    }
}
