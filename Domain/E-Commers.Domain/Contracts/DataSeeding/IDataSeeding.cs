using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.DataSeeding
{
    public interface IDataSeeding
    {
        Task seedingAsync();
        Task seedingidentityAsync();

    }
}
