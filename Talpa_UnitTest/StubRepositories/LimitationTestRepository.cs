using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class LimitationTestRepository : ILimitationRepository
    {
        List<LimitationDto> limitations;
        List<ActivityLimitationDto> activityLimitations;

        public void InitializeLimitations(List<LimitationDto> limitationDtos)
        {
            limitations = limitationDtos;
        }

        public void InitializeActivityLimitations(List<ActivityLimitationDto> activityLimitationDtos)
        {
            activityLimitations = activityLimitationDtos;
        }

        public async Task<List<LimitationDto>> GetLimitationsBySuggestionId(int suggestionId)
        {
            List<ActivityLimitationDto> activityLimitationsForSuggestion = activityLimitations.Where(al => al.SuggestionId == suggestionId).ToList();
            List<int> limitationIds = activityLimitationsForSuggestion.Select(al => al.LimitationId).ToList();
            List<LimitationDto> limitationsForSuggestion = limitations.Where(limitation => limitationIds.Contains(limitation.Id)).ToList();

            return limitationsForSuggestion;
        }

        public async Task<LimitationDto> CreateLimitationAsync(LimitationDto limitationDto)
        {
            foreach (var item in limitations)
            {
                if (limitationDto.Id == item.Id)
                {
                    return null;
                }
            }

            limitations.Add(limitationDto);
            return limitationDto;
        }

        public async Task<bool> CreateActivityLimitationAsync(ActivityLimitationDto activityLimitationDto)
        {
            foreach (var item in activityLimitations)
            {
                if (activityLimitationDto.Id == item.Id)
                {
                    return false;
                }
            }

            activityLimitations.Add(activityLimitationDto);
            return true;
        }
    }
}
