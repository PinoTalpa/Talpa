using ModelLayer.Enums;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Interfaces;
using Talpa_DAL.Repositories;

namespace Talpa_BLL.Services
{
    public class LimitationService : ILimitationService
    {
        private readonly ILimitationRepository _limitationRepository;

        public LimitationService(ILimitationRepository limitationRepository)
        {
            _limitationRepository = limitationRepository;
        }

        public async Task<List<Limitation>> GetLimitationsBySuggestionId(int suggestionId)
        {
            List<LimitationDto> limitationDtos = await _limitationRepository.GetLimitationsBySuggestionId(suggestionId);

            List<Limitation> limitations = limitationDtos.Select(limitation => new Limitation
            {
                Id = limitation.Id,
                Name = limitation.Name,
            }).ToList();

            return limitations;
        }


        public async Task<Limitation> CreateLimitationAsync(Limitation limitation)
        {
            if (string.IsNullOrEmpty(limitation.Name))
            {
                return limitation;
            }

            LimitationDto limitationDto = new()
            {
                Name = limitation.Name,
            };


            limitationDto = await _limitationRepository.CreateLimitationAsync(limitationDto);

            if (limitationDto == null)
            {
                return null;
            }

            limitation = new()
            {
                Id = limitationDto.Id,
                Name = limitationDto.Name,
            };

            return limitation;

        }

        public async Task<bool> CreateActivityLimitationAsync(Limitation limitation, int suggestionId)
        {
            if (limitation.Id <= 0 || suggestionId <= 0)
            {
                return false;
            }

            ActivityLimitationDto activityLimitation = new()
            {
                LimitationId = limitation.Id,
                SuggestionId = suggestionId,
            };

            bool isActivityLimitationCreated = await _limitationRepository.CreateActivityLimitationAsync(activityLimitation);

            if (isActivityLimitationCreated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }


}


