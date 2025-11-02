using E_Commers.Domain.Models.Products;
using E_commerse.Shared.ProductQueryParam;
using E_commerse.Shared.Sorting;
using E_Commerse.Serviceimplemention.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecpmmerce.Persistance.Specification
{
    public class ProductSpecification:BaseSpecification<Product,int>
    {
        public ProductSpecification(ProductQueryParam param) 
        : base(p=>((!param.Brandid.HasValue|| param.Brandid == p.BrandId)&&(!param.Typeid.HasValue || param.Typeid == p.TypeId))
        &&((!string.IsNullOrEmpty(param.searchvalue)||(param.searchvalue.ToLower().Contains(p.Name.ToLower()))
        )))
        {
            Addinclude(p => p.Productbrand);
            Addinclude(p => p.ProductType);
            switch(param.sorttype)
            {
                case ProductSorting.priceasc:
                    OrderbuAscfun(p => p.Price);
                    break;
                case ProductSorting.pricedsc:
                    OrderbuDescfun(p => p.Price);
                    break;
                case ProductSorting.namedsc:
                    OrderbuDescfun(p => p.Name);
                    break;
                case ProductSorting.nameasc:
                    OrderbuAscfun(p => p.Name);
                    break;
                default:
                    break;

            }    

        }
        public ProductSpecification(int id) : base(p=>p.Id==id)
        {
            Addinclude( p => p.Productbrand);
            Addinclude(p => p.ProductType);

        }
    }
}
