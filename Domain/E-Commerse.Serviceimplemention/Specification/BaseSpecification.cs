using E_Commers.Domain.Contracts.ISpecification;
using E_Commers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Specification
{
    public abstract class BaseSpecification<tentity, tkey> : ISpecification<tentity, tkey> where tentity : BaseEntity<tkey>
    {
        public  BaseSpecification(Expression<Func<tentity, bool>> cretiria)
        {
            this.cretiria = cretiria;
            
        }

        public Expression<Func<tentity, bool>> cretiria { get; private set; }

        public List<Expression<Func<tentity, object>>> includes { get; private set; } = [];

        public Expression<Func<tentity, object>> OrderbuAsc { get; private set; }

        public Expression<Func<tentity, object>> OrderbuDesc { get; private set; }

        public void OrderbuAscfun(Expression<Func<tentity, object>> OrderbuAsc)
        {
            this.OrderbuAsc = OrderbuAsc;
        }
        public void OrderbuDescfun(Expression<Func<tentity, object>> OrderbuDesc)
        {
            this.OrderbuDesc = OrderbuDesc;    
        }
        public void Addinclude(Expression<Func<tentity, object>> include)
        {
            includes.Add(include);
        }
    }
}
