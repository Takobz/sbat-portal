using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Data;
using SBAT.Infrastructure.Identity;
using SBAT.Infrastructure.Services;

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
            services.AddTransient<IRepository<Policy>, Repository<Policy>>();
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
            
            services.AddAuthorization(options => 
            {
                options.AddPolicy(RolesConstants.User, authBuilder => 
                {
                    authBuilder.RequireRole(RolesConstants.User);
                });
                options.AddPolicy(RolesConstants.MainMemeber, authBuilder => 
                {
                    authBuilder.RequireRole(RolesConstants.MainMemeber);
                });
            });

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Authentication));
        }

        public static void AddInfraServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenClaimsService, TokenClaimsService>();
            services.AddTransient<IPolicyService, PolicyService>();
        }
    }
}