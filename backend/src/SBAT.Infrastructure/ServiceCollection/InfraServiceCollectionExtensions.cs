using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Data;
using SBAT.Infrastructure.Identity;

namespace SBAT.Infrastructure.ServiceCollection
{
    //add services implemented in the infrastructure layer
    public static class InfraServiceCollectionExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SBATDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }

        public static void AddIdentityManager(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SBATDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            //to be implemented
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSection = configuration.GetSection(JwtOptions.Authentication)
                .Get<JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authenticationSection!.Issuer,
                        ValidAudience = authenticationSection!.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSection!.Key)),
                    };
                });

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Authentication));
        }

        public static void AddTokenClaimsService(this IServiceCollection services)
        {
            services.AddTransient<ITokenClaimsService, TokenClaimsService>();
        }
    }
}