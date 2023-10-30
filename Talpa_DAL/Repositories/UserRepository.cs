using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using Talpa_DAL.Data;
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

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<List<UserDto>> GetUsersAsync(string searchString)
        {
            IQueryable<UserDto> query = _dbContext.Users.Select(s => new UserDto
            {
                Id = s.Id,
                Name = s.Name,
                ProfileImage = s.ProfileImage

            });

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            IQueryable<UserDto> query = _dbContext.Users
                .Where(s => s.Id == userId)
                .Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ProfileImage = s.ProfileImage
                });

            return await query.FirstOrDefaultAsync();
        }


    }
}
