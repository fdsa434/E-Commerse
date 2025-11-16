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
        private int maxsize = 10;
        private int defaultsize = 5;
        private int _pageSize;

        public int? Typeid { get; set; }
        public int? Brandid { get; set; }

        public int PageSize
        {
            get => _pageSize == 0 ? defaultsize : _pageSize;
            set => _pageSize = value > maxsize ? maxsize : value;
        }

        public int PageIndex { get; set; } = 1;
        public string? searchvalue { get; set; }
        public ProductSorting? sorttype { get; set; }
    }
}
