using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_BLL.Models
{
    [Keyless]
    public class UserActivityDate
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int ActivityDateId { get; set; }
        public ActivityDate ActivityDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
