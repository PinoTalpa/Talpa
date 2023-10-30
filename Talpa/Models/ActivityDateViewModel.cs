using ModelLayer.Models;

namespace Talpa.Models
{
    public class ActivityDateViewModel
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public VoteDto Suggestion { get; set; }

        public bool IsSelected { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
