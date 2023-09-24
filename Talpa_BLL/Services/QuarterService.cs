using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;

namespace Talpa_BLL.Services
{
    public class QuarterService : IQuarterService
    {
        public QuarterDay GetQuarterDays()
        {
            DateTime today = DateTime.Today;
            int quarter = (today.Month - 1) / 3 + 1;
            DateTime quarterStart = new(today.Year, (quarter - 1) * 3 + 1, 1);

            DateTime quarterEnd = quarterStart.AddMonths(3).AddDays(-1);

            List<DateTime> quarterDays = new();
            for (DateTime date = quarterStart; date <= quarterEnd; date = date.AddDays(1))
            {
                quarterDays.Add(date);
            }

            return new QuarterDay
            {
                Days = quarterDays
            };
        }
    }
}
