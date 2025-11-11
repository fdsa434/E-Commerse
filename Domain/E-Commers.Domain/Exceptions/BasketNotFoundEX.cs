using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Exceptions
{
    public class BasketNotFoundEX(string id ): NotFoundException($"basket with {id} is not found")
    {
    }
}
