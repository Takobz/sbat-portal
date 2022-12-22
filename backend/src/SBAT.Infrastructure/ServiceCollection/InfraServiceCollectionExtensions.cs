using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SBAT.Infrastructure.Data;

namespace SBAT.Infrastructure.ServiceCollection
{
    public static class InfraServiceCollectionExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SBATDbContext>(options => 
            {
                options.UseSqlite(connectionString);
            });
        }
    }
}