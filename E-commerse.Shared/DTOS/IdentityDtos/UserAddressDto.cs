using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared.DTOS.IdentityDtos
{
    public class UserAddressDto
    {
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Country { get; set; } = null!;
    }

}
