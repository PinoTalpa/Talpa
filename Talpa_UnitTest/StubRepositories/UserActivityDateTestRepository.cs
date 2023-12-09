using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class UserActivityDateTestRepository : IUserActivityDateRepository
    {
        private List<UserActivityDateDto> userActivityDates;

        public void InitializeUserActivityDates(List<UserActivityDateDto> userActivityDateDtos)
        {
            userActivityDates = userActivityDateDtos;
        }

        public async Task<bool> CreateUserActivityDateAsync(UserActivityDateDto newUserActivityDate)
        {
            try
            {
                userActivityDates.Add(newUserActivityDate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
