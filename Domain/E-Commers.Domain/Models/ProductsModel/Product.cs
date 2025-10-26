using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Type_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commers.Domain.Models.Products
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Productbrand { get; set; }
        public int TypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

    }
}
