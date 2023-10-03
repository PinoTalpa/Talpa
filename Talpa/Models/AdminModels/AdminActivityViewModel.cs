using ModelLayer.Enums;

namespace Talpa.Models.AdminModels
{
    public class AdminActivityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Date { get; set; }
        public ActivityState ActivityState { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
