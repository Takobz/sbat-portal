using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Data;

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

        public static void AddRepositories(this IServiceCollection services)
        {
            //to be implemented
        }
    }
}