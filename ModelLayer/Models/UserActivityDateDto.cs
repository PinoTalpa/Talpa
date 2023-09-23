using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    [Keyless]
    public class UserActivityDateDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }

        public int ActivityDateId { get; set; }
        public ActivityDateDto ActivityDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
