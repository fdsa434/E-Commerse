using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared.ErrorModels
{
    public class ValidationErrortoreturn
    {
        public int Statuscode { get; set; } = (int)HttpStatusCode.BadRequest;

        public string Massege { get; set; } = "validation error";
        public IEnumerable<ValidationErorr> validationerrors { get; set; }



    }
}
