namespace Talpa.Models
{
    public class SuggestionLimitationViewModel
    {
        public SuggestionViewModel Suggestion { get; set; }
        public List<LimitationViewModel> limitations { get; set; }
    }
}
