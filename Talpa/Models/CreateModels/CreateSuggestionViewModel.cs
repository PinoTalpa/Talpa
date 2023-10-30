using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace Talpa.Models.CreateModels
{
    public class CreateSuggestionViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "NameMaxLengthError")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
