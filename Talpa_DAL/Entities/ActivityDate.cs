using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Entities
{
    public class ActivityDate
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int QuarterId { get; set; }
        public Quarter Quarter { get; set; }

        public DateTime Date { get; set; }
    }
}
