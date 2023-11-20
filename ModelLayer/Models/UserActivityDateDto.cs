using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class UserActivityDateDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public UserDto User { get; set; }

        public int ActivityDateId { get; set; }
        public ActivityDateDto ActivityDate { get; set; }

        public bool IsAvailable { get; set; }

        public static implicit operator UserActivityDateDto(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
