using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Data;
using Talpa_DAL.Interfaces;

namespace Talpa_DAL.Repositories
{
    public class UserActivityDateRepository : IUserActivityDateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserActivityDateRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateUserActivityDateAsync(UserActivityDateDto userActivityDateDto)
        {
            try
            {
                _dbContext.UserActivityDates.Add(userActivityDateDto);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
