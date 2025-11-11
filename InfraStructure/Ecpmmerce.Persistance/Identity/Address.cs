using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Identity
{
    public class Address
    {
        public string fname { get; set; } = null!;
        public string lname { get; set; } = null!;
        public string street { get; set; }
        public string country { get; set; }
        [ForeignKey("user")]
        public string userid { get; set; }
        public ApplicationUser user { get; set; }




    }
}
