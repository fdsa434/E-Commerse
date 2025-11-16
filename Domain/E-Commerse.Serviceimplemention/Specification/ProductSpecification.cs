using E_Commers.Domain.Models.Products;
using E_Commerse.Serviceimplemention.Specification;
using E_commerse.Shared.ProductQueryParam;
using E_commerse.Shared.Sorting;

public class ProductSpecification : BaseSpecification<Product, int>
{
    public ProductSpecification(ProductQueryParam param)
        : base(p =>
            (!param.Brandid.HasValue || param.Brandid == p.BrandId) &&
            (!param.Typeid.HasValue || param.Typeid == p.TypeId) &&
            (string.IsNullOrEmpty(param.searchvalue) ||
             p.Name.ToLower().Contains(param.searchvalue.ToLower()))
        )
    {
        Addinclude(p => p.Productbrand);
        Addinclude(p => p.ProductType);

        // ✔ pagination صح
        Addpagination(param.PageIndex, param.PageSize);

        // ✔ sorting
        switch (param.sorttype)
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
        }
    }

    public ProductSpecification(int id)
        : base(p => p.Id == id)
    {
        Addinclude(p => p.Productbrand);
        Addinclude(p => p.ProductType);
    }
}
