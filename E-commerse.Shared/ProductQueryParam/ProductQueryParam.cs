using E_commerse.Shared.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerse.Shared.ProductQueryParam
{
    public class ProductQueryParam
    {
        public int? Typeid { get; set; }
        public int? Brandid { get; set; }
        public String? searchvalue { get; set; }

        public ProductSorting? sorttype { get; set; }

    }
}
