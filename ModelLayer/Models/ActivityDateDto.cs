namespace ModelLayer.Models
{
    public class ActivityDateDto
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public SuggestionDto Suggestion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
