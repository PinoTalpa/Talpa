using ModelLayer.Models;

namespace Talpa.Models;

public class PlannedActivityViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime Date { get; set; }
}
