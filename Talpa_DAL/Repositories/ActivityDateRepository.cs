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
    public class ActivityDateRepository : IActivityDateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ActivityDateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateActivityDates(List<ActivityDateDto> activityDates)
        {
            try
            {
                foreach (ActivityDateDto activityDate in activityDates)
                {
                    _dbContext.ActivityDates.Add(activityDate);
                }

                _dbContext.SaveChanges();
            }
            catch
            {
                // throw exception or som
            }
        }
    }
}
