using E_Commers.Domain.Identity;
using E_Commers.Domain.Models.BrandModel;
using E_Commers.Domain.Models.Products;
using E_Commers.Domain.Models.Type_Model;
using Ecpmmerce.Persistance.Context.identitycontext;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Microsoft.AspNetCore.Identity;
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
        private readonly Identitydbcontext contextidentity;
        private readonly UserManager<ApplicationUser> user;
        private readonly RoleManager<IdentityRole> role;

        public DataSeeding(StorDBContext context, Identitydbcontext contextidentity,UserManager<ApplicationUser>user,RoleManager<IdentityRole> role)
        {
            this.context = context;
            this.contextidentity = contextidentity;
            this.user = user;
            this.role = role;
        }

        
       public async Task seedingAsync()
        {
            // Apply pending migrations
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }

            // Seeding Brands
            if (!await context.Brands.AnyAsync())
            {
                var brandsJson = await File.ReadAllTextAsync(@"..\InfraStructure\Ecpmmerce.Persistance\Data\brands.json");
                var brandData = JsonSerializer.Deserialize<List<Brand>>(brandsJson);

                if (brandData != null && brandData.Any())
                {
                    context.Brands.AddRange(brandData);
                    await context.SaveChangesAsync();
                }

            }

            if (!await context.types.AnyAsync())
            {
                var typesJson = await File.ReadAllTextAsync(@"..\InfraStructure\Ecpmmerce.Persistance\Data\types.json");
                var typeData = JsonSerializer.Deserialize<List<ProductType>>(typesJson);
                if (typeData != null && typeData.Any())
                {
                    context.types.AddRange(typeData);
                    await context.SaveChangesAsync();

                }
            }

            
            if (!await context.products.AnyAsync())
            {
                var productsJson = await File.ReadAllTextAsync(@"..\InfraStructure\Ecpmmerce.Persistance\Data\products.json");
                var productData = JsonSerializer.Deserialize<List<Product>>(productsJson);

                if (productData != null && productData.Any())
                {

                    context.products.AddRange(productData);


                    await context.SaveChangesAsync();


                }
            }
        }




        public async Task seedingidentityAsync()
        {
            var mig2 = await contextidentity.Database.GetPendingMigrationsAsync();
            if (mig2.Any())
            {
                contextidentity.Database.Migrate();
            }
            if (!role.Roles.Any())
            {
               await role.CreateAsync(new IdentityRole("Admin"));
              await role.CreateAsync(new IdentityRole("SuperAdmin"));

            }
            if (!user.Users.Any())
            {
                var user1 = new ApplicationUser()
                {
                    UserName = "mohamedAhmed",
                    displayname = "mohamed Ahmed",
                    Email = "mohamedaramadn130@gmail.com",
                    PhoneNumber = "01030388214",

                };
                var user2 = new ApplicationUser()
                {
                    UserName = "AliAhmed",
                    displayname = "Ali Ahmed",
                    Email = "Ali@gmail.com",
                    PhoneNumber = "01020288241",
                    

                };
               
                await user.CreateAsync(user1, "M0h@m3d#2025!Str0ng$Admin");
                await user.CreateAsync(user2, "Al!#S3cur3$2025&Sup3rP@ss");
                var dbUser1 = await user.FindByEmailAsync("mohamedaramadn130@gmail.com");
                var dbUser2 = await user.FindByEmailAsync("Ali@gmail.com");
                await user.AddToRoleAsync(dbUser1, "Admin");
                await user.AddToRoleAsync(dbUser2, "SuperAdmin");



            }
            await contextidentity.SaveChangesAsync();
        }
    }
}
