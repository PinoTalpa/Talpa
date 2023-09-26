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
        public QuarterDay GetQuarterDays(string selectedQuarter)
        {
            DateTime selectedQuarterDate = DateTime.Parse(selectedQuarter);

            DateTime today = selectedQuarterDate;
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

        public List<Quarter> GetUpcomingQuarters()
        {
            DateTime currentDate = DateTime.Now;
            List<Quarter> upcomingQuarters = new();

            for (int i = 0; i < 4; i++)
            {
                int quarterMonth = (currentDate.Month - 1) / 3 * 3 + 1;

                DateTime startOfQuarter = new(currentDate.Year, quarterMonth, currentDate.Day);

                int quarterNumber = ((quarterMonth - 1) / 3) + 1;
                string quarterName = $"Q{quarterNumber} {startOfQuarter.Year}";

                Quarter quarter = new Quarter
                {
                    Name = quarterName,
                    Quarters = new List<DateTime> { startOfQuarter }
                };

                upcomingQuarters.Add(quarter);

                currentDate = currentDate.AddMonths(3);
            }

            return upcomingQuarters;
        }
    }
}
