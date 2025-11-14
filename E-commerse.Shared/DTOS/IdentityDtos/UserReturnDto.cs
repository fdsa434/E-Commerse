using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared.DTOS.IdentityDtos
{
    public class UserReturnDto
    {
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        public string token { get; set; } = null!;


    }
}
