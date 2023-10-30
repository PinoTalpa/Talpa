using ModelLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public List<SuggestionDto>? Suggestions { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        /*        public string? ErrorMessage { get; set; }*/
    }
}
