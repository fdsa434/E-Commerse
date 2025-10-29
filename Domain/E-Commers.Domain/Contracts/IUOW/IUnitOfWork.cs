using E_Commers.Domain.Contracts.Reposatory.IGenericRepo;
using E_Commers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.IUOW
{
    public interface IUnitOfWork
    {
        public IGenericReposatory<tentity,tkey> Gneraterepo<tentity, tkey>()
            where tentity:BaseEntity<tkey>;
        public Task<int> Savechangesasync();
    }
}
