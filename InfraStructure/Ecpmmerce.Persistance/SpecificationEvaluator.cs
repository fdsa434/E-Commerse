using E_Commers.Domain.Contracts.ISpecification;
using E_Commers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecpmmerce.Persistance
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<tentity>  generatequery<tentity, tkey>(IQueryable<tentity> basequery, ISpecification<tentity, tkey> spec)
            where tentity : BaseEntity<tkey>
        {
            var querey = basequery;
            if(spec.cretiria is not null)
            {
                querey = querey.Where(spec.cretiria); 
            }
            if (spec.OrderbuAsc is not null)
            {
                querey = querey.OrderBy(spec.OrderbuAsc);
            }
            if (spec.OrderbuDesc is not null)
            {
                querey = querey.OrderByDescending(spec.OrderbuDesc);
            }
            if (spec.ispaginated )
            {
                querey = querey.Take(spec.take).Skip(spec.skip);
            }
            if (spec.includes is not null)
            {
                querey = spec.includes.Aggregate(querey, (currquery, ex) => currquery.Include(ex));
            }
            return querey;
        }
        
    }
}
