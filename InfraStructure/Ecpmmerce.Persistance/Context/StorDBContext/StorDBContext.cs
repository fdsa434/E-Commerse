using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Products;
using E_Commers.Domain.Models.Type_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecpmmerce.Persistance.Context.StorDBContext
{
    public class StorDBContext:DbContext
    {
        public StorDBContext(DbContextOptions<StorDBContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
         .Property(b => b.Id)
         .ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductType>()
        .Property(b => b.Id)
        .ValueGeneratedOnAdd();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StorDBContext).Assembly);
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductType> types { get; set; }

    }
}
