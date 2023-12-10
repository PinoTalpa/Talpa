using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;
using Talpa_DAL.Repositories;

namespace Talpa_UnitTest.StubRepositories
{
    public class UserTestRepository : IUserRepository
    {
        private List<UserDto> users = new();

        public void InitializeUsers(List<UserDto> userDtos)
        {
            users = userDtos;
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            UserDto user = new();

            foreach (var item in users)
            {
                if (item.Id == userId)
                {
                    user = item;
                    break;
                }
            }

            return user;
        }

        public async Task Login(string userId, string name, string email)
        {
            UserDto userDto = new()
            {
                Id = userId,
                Name = name,
                Email = email
            };

            users.Add(userDto);
        }

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (user.Id == users[i].Id)
                {
                    users[i] = user;
                    return true;
                }
            }

            return false;
        }

        public UserDto? GetUser()
        {
            return users.FirstOrDefault();
        }
    }
}
