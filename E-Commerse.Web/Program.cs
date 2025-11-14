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
using Ecpmmerce.Persistance.Reposatory.GenericReposatoty;
using Ecpmmerce.Persistance.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerse.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllers();

           
            builder.Services.AddDbContext<StorDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<Identitydbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("identityconnection")));

         
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Identitydbcontext>()
                .AddDefaultTokenProviders();

           

            var jwtSettings = builder.Configuration.GetSection("Jwt");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

          
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IserviceManager, ServiceManager>();
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("localhost")));

            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ProductProfile).Assembly));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationErorr
                        {
                            feild = m.Key,
                            errors = m.Value.Errors.Select(e => e.ErrorMessage)
                        });

                    var response = new ValidationErrortoreturn
                    {
                        validationerrors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

          
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var seedingService = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                seedingService.seedingAsync().GetAwaiter().GetResult();
                seedingService.seedingidentityAsync().GetAwaiter().GetResult();
            }

           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();   
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
