using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talpa_DAL.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
