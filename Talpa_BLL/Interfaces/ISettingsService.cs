using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface ISettingsService
    {
        Task<Settings> GetSettings();
        Task<Settings> UpdateSettings(Settings settings);
    }
}
