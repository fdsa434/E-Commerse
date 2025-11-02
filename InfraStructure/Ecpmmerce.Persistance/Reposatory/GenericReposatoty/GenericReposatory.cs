using E_Commers.Domain.Contracts.ISpecification;
using E_Commers.Domain.Contracts.Reposatory.IGenericRepo;
using E_Commers.Domain.Models;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecpmmerce.Persistance.Reposatory.GenericReposatoty
{
    public class GenericReposatory<tentity, tkey> : IGenericReposatory<tentity, tkey> where tentity : BaseEntity<tkey>
    {
        private readonly StorDBContext context;

        public GenericReposatory(StorDBContext context)
        {
            this.context = context;
        }

        public void AddEntity(tentity item)
        {
            context.Set<tentity>().Add(item);
        }

        public void DeleteAddEntity(tentity item)
        {
            context.Set<tentity>().Remove(item);

        }

        public async Task<tentity> getallbuidrepo(tkey Id)
        {
            return await context.Set<tentity>().FindAsync(Id);
        }

        public async Task<IEnumerable<tentity>> getallrepo()
        {
            return await context.Set<tentity>().ToListAsync();

        }
        public async Task<IEnumerable<tentity>> getallspecificationrepo(ISpecification<tentity, tkey> spec)
        {
            var basequery = context.Set<tentity>();
            return await SpecificationEvaluator.generatequery<tentity, tkey>(basequery, spec).ToListAsync();
        }
        public async Task<tentity> getallspecificationByidrepo(ISpecification<tentity, tkey> spec)
        {

            var basequery = context.Set<tentity>();
            return await SpecificationEvaluator.generatequery<tentity, tkey>(basequery, spec).FirstOrDefaultAsync();

        }

       

        public void UpdateEntity(tentity item)
        {
            context.Set<tentity>().Update(item);

        }
    }
}
