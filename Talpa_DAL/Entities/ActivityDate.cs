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
        public int SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; }

        public DateTime Date { get; set; }
    }
}
