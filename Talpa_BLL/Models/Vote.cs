using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_BLL.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public int SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; }
        public string? ErrorMessage { get; internal set; }
    }
}
