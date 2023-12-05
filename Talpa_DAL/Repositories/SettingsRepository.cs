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
    public class SettingsRepository : ISettingsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SettingsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SettingsDto> GetSettings()
        {
            SettingsDto settings = null;

            try
            {
                settings = _dbContext.Settings.FirstOrDefault();
            }
            catch (Exception)
            {
                settings = null;
            }

            return settings;
        }

        public async Task<SettingsDto> UpdateSettings(SettingsDto settingsDto)
        {
            try
            {
                _dbContext.Settings.Update(settingsDto);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return settingsDto;
            }

            return settingsDto;
        }
    }
}
