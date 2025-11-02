using E_Commers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.ISpecification
{
    public interface ISpecification<tentity,tkey> where tentity:BaseEntity<tkey>
    {
        public Expression<Func<tentity,bool>> cretiria { get; }
        public Expression<Func<tentity, Object>> OrderbuAsc { get; }
        public Expression<Func<tentity, Object>> OrderbuDesc { get; }

        public List<Expression<Func<tentity, object>>> includes { get; }

    }
}
