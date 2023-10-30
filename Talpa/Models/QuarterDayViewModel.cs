namespace Talpa.Models
{
    public class QuarterDayViewModel
    {
        public int SuggestionId { get; set; }
        public List<DateTime> Days { get; set; }

        public string? SelectedQuarter { get; set; }
    }
}
