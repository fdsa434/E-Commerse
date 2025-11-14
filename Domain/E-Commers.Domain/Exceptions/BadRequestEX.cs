using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Exceptions
{
    public class BadRequestEX(List<string>errors):Exception("Validation Faild")
    {
        public List<string> errors = errors;
    }
}
