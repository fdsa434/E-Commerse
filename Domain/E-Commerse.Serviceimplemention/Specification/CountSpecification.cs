using E_Commers.Domain.Models.Products;
using E_commerse.Shared.ProductQueryParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Specification
{
    public class CountSpecification:BaseSpecification<Product,int>
    {
        public CountSpecification(ProductQueryParam param):base(p => ((!param.Brandid.HasValue || param.Brandid == p.BrandId) && (!param.Typeid.HasValue || param.Typeid == p.TypeId))
        && ((!string.IsNullOrEmpty(param.searchvalue) || (param.searchvalue.ToLower().Contains(p.Name.ToLower()))
        )))
        {

        }
    }
}
