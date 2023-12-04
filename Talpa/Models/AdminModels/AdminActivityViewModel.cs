using Talpa_BLL.Models;

namespace Talpa.Models.AdminModels
{
    public class AdminActivityViewModel
    {
        public int ActivityId { get; set; }
        public int OtherSuggestionId1 { get; set; }
        public int OtherSuggestionId2 { get; set; }
        public int SuggestionId { get; set; }
        public List<SuggestionViewModel>? Suggestions { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
