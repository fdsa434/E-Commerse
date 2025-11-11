


using E_Commers.Domain.Contracts.DataSeeding;
using E_Commers.Domain.Contracts.IUOW;
using E_Commers.Domain.Contracts.Reposatory.BasketRepo;
using E_Commers.Domain.Identity;
using E_commerse.Shared;
using E_commerse.Shared.ErrorModels;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using E_Commerse.Serviceimplemention.Profiles;
using E_Commerse.Serviceimplemention.Serviceimplemetition.ServiceManager;
using Ecpmmerce.Persistance.Context.identitycontext;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Ecpmmerce.Persistance.Reposatory.GenericReposatoty;
using Ecpmmerce.Persistance.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerse.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddDbContext<StorDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddDbContext<StorDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("E-CommerseIdentityDBv")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Identitydbcontext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IserviceManager, ServiceManager>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("localhost"));
            });
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();


            builder.Services.AddAutoMapper(p => p.AddMaps(typeof(ProductProfile).Assembly));

            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Where(m => m.Value.Errors.Any())
                      .Select(m => new ValidationErorr()
                      {
                          feild = m.Key,
                          errors = m.Value.Errors.Select(e => e.ErrorMessage)
                      });
                    var response = new ValidationErrortoreturn()
                    {
                        validationerrors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var objectscope = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            objectscope.seedingAsync();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
