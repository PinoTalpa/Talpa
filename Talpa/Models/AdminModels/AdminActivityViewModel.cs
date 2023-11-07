using Talpa_BLL.Models;

namespace Talpa.Models.AdminModels
{
    public class AdminActivityViewModel
    {
        public List<SuggestionViewModel>? Suggestions { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
