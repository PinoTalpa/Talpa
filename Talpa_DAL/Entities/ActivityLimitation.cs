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
        public int SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; }

        public int LimitationId { get; set; }
        public Limitation Limitation { get; set; }
    }
}
