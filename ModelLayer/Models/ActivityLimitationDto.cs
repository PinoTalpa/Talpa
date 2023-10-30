using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class ActivityLimitationDto
    {
        public int Id { get; set; }

        public int SuggestionId { get; set; }
        public SuggestionDto Suggestion { get; set; }

        public int LimitationId { get; set; }
        public LimitationDto Limitation { get; set; }
    }
}
