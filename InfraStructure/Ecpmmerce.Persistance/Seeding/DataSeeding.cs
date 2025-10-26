using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Products;
using E_Commers.Domain.Models.Type_Model;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Ecpmmerce.Persistance.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commers.Domain.Contracts.DataSeeding
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StorDBContext context;

        public DataSeeding(StorDBContext context)
        {
            this.context = context;
        }

        public void seeding()
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.brands.Any())
            {
                var brands = File.ReadAllText(@"..\InfraStructure\Ecpmmerce.Persistance\Data\brands.json");
                var productbrands = JsonSerializer.Deserialize<List<Brand>>(brands);
                if(productbrands is not null&& productbrands.Any())
                {
                    context.brands.AddRange(productbrands);
                }
            }
            if (!context.types.Any())
            {
                var types = File.ReadAllText(@"..\InfraStructure\Ecpmmerce.Persistance\Data\types.json");
                var producttypes = JsonSerializer.Deserialize<List<ProductType>>(types);
                if (producttypes is not null && producttypes.Any())
                {
                    context.types.AddRange(producttypes);
                }
            }
            context.SaveChanges();

            if (!context.products.Any())
            {
                var a = File.ReadAllText(@"..\InfraStructure\Ecpmmerce.Persistance\Data\products.json");
                var productelement = JsonSerializer.Deserialize<List<Product>>(a);
                if (productelement is not null && productelement.Any())
                {
                    context.products.AddRange(productelement);
                }
            }
            context.SaveChanges();

        }
    }
}
