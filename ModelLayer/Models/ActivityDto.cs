using ModelLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public ActivityState ActivityState { get; set; }

/*        public string? ErrorMessage { get; set; }*/
    }
}
