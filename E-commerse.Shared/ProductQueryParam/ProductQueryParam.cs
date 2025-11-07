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
        private int maxsize=10;
        private int defaultsize =5;
        public int? Typeid { get; set; }
        public int? Brandid { get; set; }
        private int pagesize { get { return pagesize; } set { pagesize = value > maxsize ? maxsize : value; } }

        public int PageSize { get { return pagesize; } set { PageSize = value==0 ? defaultsize : pagesize; } }
        public int PageIndex { get; set; } = 1;

        public String? searchvalue { get; set; }

        public ProductSorting? sorttype { get; set; }

    }
}
