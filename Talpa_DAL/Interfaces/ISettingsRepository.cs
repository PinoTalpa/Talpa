using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Interfaces
{
    public interface ISettingsRepository
    {
        Task<SettingsDto> GetSettings();
        Task<SettingsDto> UpdateSettings(SettingsDto settings);
    }
}
