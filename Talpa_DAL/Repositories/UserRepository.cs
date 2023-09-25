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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task Login(string userId, string name, string email)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (existingUser == null)
            {
                var newUser = new UserDto
                {
                    Id = userId,
                    Name = name,
                    Email = email
                };

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
