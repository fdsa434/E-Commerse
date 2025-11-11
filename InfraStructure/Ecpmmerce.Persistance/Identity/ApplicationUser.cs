using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Id { get; set; }
        public string displayname { get; set; } = null!;
        public Address address { get; set; }
    }
}
