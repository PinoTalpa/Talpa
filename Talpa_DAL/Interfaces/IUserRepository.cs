using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Interfaces
{
    public interface IUserRepository
    {
        Task Login(string userId, string name, string email);
        //Task<List<UserDto>> GetSuggestionsAsync(string searchString);
        Task<bool> UpdateUserAsync(UserDto user);






    }
}
