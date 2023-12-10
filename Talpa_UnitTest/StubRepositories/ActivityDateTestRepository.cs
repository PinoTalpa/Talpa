using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class ActivityDateTestRepository : IActivityDateRepository
    {
        List<ActivityDateDto> activityDates = new();

        public void InitializeActivityDates(List<ActivityDateDto> activityDateDtos)
        {
            activityDates = activityDateDtos;
        }

        public async Task CreateActivityDates(List<ActivityDateDto> newActivityDates)
        {
            try
            {
                foreach (var activityDate in newActivityDates)
                {
                    activityDates.Add(activityDate);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating activity dates", ex);
            }
        }

        public Task<List<ActivityDateDto>> GetActivitiesDateWithId(int selectedSuggestionId)
        {
            try
            {
                var result = activityDates
                    .Where(ad => ad.SuggestionId == selectedSuggestionId)
                    .ToList();

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving activity dates", ex);
            }
        }
    }
}
