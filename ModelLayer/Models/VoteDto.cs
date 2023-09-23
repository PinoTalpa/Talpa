using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class VoteDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }

        public int SuggestionId { get; set; }
        public SuggestionDto Suggestion { get; set; }
    }
}
