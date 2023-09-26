using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_BLL.Models
{
    public class TimeInput
    {
        public DateTime Date { get; set; }
        public int StartTimeHours { get; set; }
        public int StartTimeMinutes { get; set; }
        public int EndTimeHours { get; set; }
        public int EndTimeMinutes { get; set; }
    }
}
