using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Interfaces
{
    public interface IActivityDateRepository
    {
        Task CreateActivityDates(List<ActivityDateDto> activityDates);
    }
}
