using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared.DTOS.IdentityDtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
