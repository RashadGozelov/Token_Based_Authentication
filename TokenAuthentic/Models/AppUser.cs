using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthentic.Models
{
    public class AppUser:IdentityUser
    {
        public string City { get; set; }

        public DateTime? Datetime { get; set; }

        public int Gender { get; set; }

        public string Picture { get; set; }
    }
}
