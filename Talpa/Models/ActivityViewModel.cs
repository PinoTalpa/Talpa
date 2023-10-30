using Talpa_DAL.Entities;
using Talpa_DAL.Enums;

namespace Talpa.Models
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string ImageUrl { get; set; }
        public ActivityState ActivityState { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
