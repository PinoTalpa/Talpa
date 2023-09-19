using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Entities
{
    [Keyless]
    public class ActivityLimitation
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int LimitationId { get; set; }
        public Limitation Limitation { get; set; }
    }
}
