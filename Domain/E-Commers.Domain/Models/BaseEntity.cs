using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Models
{
    public class BaseEntity<tkey>
    {
        public tkey Id { get; set; }
    }
}
