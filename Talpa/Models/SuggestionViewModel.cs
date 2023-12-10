using Talpa_BLL.Models;
using Talpa_DAL.Enums;

namespace Talpa.Models
{
    public class SuggestionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Date { get; set; }
        public ActivityState ActivityState { get; set; }
        public int? VoteCount { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
