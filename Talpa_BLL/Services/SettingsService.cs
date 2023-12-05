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
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<Settings> GetSettings()
        {
            SettingsDto settingsDto = await _settingsRepository.GetSettings();

            Settings newSettings = new()
            {
                Id = settingsDto.Id,
                PrimaryColor = settingsDto.PrimaryColor,
                SecondaryColor = settingsDto.SecondaryColor,    
                BackgroundColor = settingsDto.BackgroundColor,
            };

            return newSettings;
        }

        public async Task<Settings> UpdateSettings(Settings settings)
        {
            SettingsDto settingsDto = new()
            {
                Id = settings.Id,
                PrimaryColor = settings.PrimaryColor,
                SecondaryColor = settings.SecondaryColor,
                BackgroundColor = settings.BackgroundColor,
            };

            settingsDto = await _settingsRepository.UpdateSettings(settingsDto);

            if (settingsDto != null)
            {
                return settings;
            }
            else
            {
                return null;
            }
        }
    }
}
