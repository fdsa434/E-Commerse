using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Models.BasketModel
{
    public class BasketCustomer
    {
        public string Id { get; set; }
        public ICollection<BasketItems>items  { get; set; }

    }
}
