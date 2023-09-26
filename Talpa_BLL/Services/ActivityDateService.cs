using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Interfaces;

namespace Talpa_BLL.Services
{
    public class ActivityDateService : IActivityDateService
    {
        private readonly IActivityDateRepository _activityDateRepository;

        public ActivityDateService(IActivityDateRepository activityDateRepository)
        {
            _activityDateRepository = activityDateRepository;
        }

        public async Task CreateActivityDates(List<ActivityDate> activityDates)
        {
            List<ActivityDateDto> activityDateDtos = activityDates.Select(ad => new ActivityDateDto
            {
                SuggestionId = ad.SuggestionId,
                StartDate = ad.StartDate,
                EndDate = ad.EndDate,
            }).ToList();

            await _activityDateRepository.CreateActivityDates(activityDateDtos);
        }
    }
}
