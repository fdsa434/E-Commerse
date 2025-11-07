using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared
{
    public class ValidationErorr
    {
        public string feild { get; set; } = null!;
        public IEnumerable<string>errors { get; set; }
    }
}
