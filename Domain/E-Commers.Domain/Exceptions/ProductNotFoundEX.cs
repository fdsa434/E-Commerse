using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Exceptions
{
    public class ProductNotFoundEX(int id):NotFoundException($"product with id{id} not found")
    {
    }
}
