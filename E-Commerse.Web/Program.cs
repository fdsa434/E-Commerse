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

            // Controllers
            builder.Services.AddControllers();

            // Database Contexts
            builder.Services.AddDbContext<StorDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<Identitydbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("identityconnection")));

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Identitydbcontext>()
                .AddDefaultTokenProviders();

            // Dependency Injection
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IserviceManager, ServiceManager>();
            builder.Services.AddScoped<IBasketReposatory, BasketReposatory>();

            // Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redisdatabase"));
            });

            // AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ProductProfile(builder.Configuration));
                cfg.AddProfile(new BasketMappingProfile());
            });

            // API Validation Response
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

            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])
                    ),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // Data Seeding
            using (var scope = app.Services.CreateScope())
            {
                var seedingService = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                seedingService.seedingAsync().GetAwaiter().GetResult();
                seedingService.seedingidentityAsync().GetAwaiter().GetResult();
            }

            // Middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
