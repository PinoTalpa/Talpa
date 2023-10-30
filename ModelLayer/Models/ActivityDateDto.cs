using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class ActivityDateDto
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public VoteDto Suggestion { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
