using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_DAL.Interfaces;

namespace Talpa_UnitTest.StubRepositories
{
    public class SettingsTestRepository : ISettingsRepository
    {
        SettingsDto settings;

        public void InitializeSettings(SettingsDto settingDto)
        {
            settings = settingDto;
        }

        public async Task<SettingsDto> GetSettings()
        {
            return settings;
        }

        public async Task<SettingsDto> UpdateSettings(SettingsDto settings)
        {
            settings.PrimaryColor = settings.PrimaryColor;
            settings.SecondaryColor = settings.SecondaryColor;
            settings.BackgroundColor = settings.BackgroundColor;

            return settings;
        }
    }
}
