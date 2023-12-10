namespace Talpa_BLL.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public List<Suggestion>? Suggestions { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
