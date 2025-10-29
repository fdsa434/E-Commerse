using E_Commers.Domain.Contracts.IUOW;
using E_Commers.Domain.Contracts.Reposatory.IGenericRepo;
using E_Commers.Domain.Models;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Ecpmmerce.Persistance.Reposatory.GenericReposatoty;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecpmmerce.Persistance.UnitOfWork
{
    public class UnitOfWork(StorDBContext context) : IUnitOfWork
    {
        Dictionary<string, object> repos = [];
        public IGenericReposatory<tentity, tkey> Gneraterepo<tentity, tkey>() where tentity : BaseEntity<tkey>
        {
            var tablename = nameof(tentity).ToString();
            if (repos.ContainsKey(tablename))
            {
                return (IGenericReposatory<tentity, tkey>)repos[tablename];
            }
            else
            {
                var repo = new GenericReposatory<tentity, tkey>(context);
                repos.Add(tablename, repo);
                return repo;

            }
        }

        public async Task<int> Savechangesasync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
