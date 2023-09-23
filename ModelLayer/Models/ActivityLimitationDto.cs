using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    [Keyless]
    public class ActivityLimitationDto
    {
        public int SuggestionId { get; set; }
        public SuggestionDto Suggestion { get; set; }

        public int LimitationId { get; set; }
        public LimitationDto Limitation { get; set; }
    }
}
