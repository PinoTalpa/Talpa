using Talpa_DAL.Entities;
using Talpa_DAL.Enums;

namespace Talpa.Models
{
    public class SuggestionLeaderboardViewModel
    {
        public List<LeaderboardViewModel> leaderboardViewModels = new List<LeaderboardViewModel>();
        public string? ErrorMessage { get; set; }
    }
}
