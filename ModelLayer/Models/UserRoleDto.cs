using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    [Keyless]
    public class UserRoleDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }

        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
