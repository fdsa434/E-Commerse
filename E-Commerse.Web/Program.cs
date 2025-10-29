


using E_Commers.Domain.Contracts.DataSeeding;
using E_Commers.Domain.Contracts.IUOW;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using E_Commerse.Serviceimplemention.Profiles;
using E_Commerse.Serviceimplemention.Serviceimplemetition.ServiceManager;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Ecpmmerce.Persistance.Context.StorDBContext;
using Ecpmmerce.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<StorDBContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IserviceManager, ServiceManager>();

            builder.Services.AddAutoMapper(p=>p.AddMaps(typeof(ProductProfile).Assembly));


            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var objectscope = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            objectscope.seedingAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
