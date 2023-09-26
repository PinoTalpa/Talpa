using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;

namespace Talpa_BLL.Interfaces
{
    public interface IActivityDateService
    {
        Task CreateActivityDates(List<ActivityDate> activityDates);
    }
}
