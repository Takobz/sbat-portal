using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SBAT.Infrastructure.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<SBATDbContext>
    {
        /*
        dotnet 5+ pass argument 0 as environment and 1 as assembly to do migration in: Development or empty string for prod 
        eg) using dotnet cli: dotnet ef migrations add InitialCreate -- Development
        */
        /**/
        //run this from the project that has the database connection string i.e SBAT.Web in this instance
        public SBATDbContext CreateDbContext(string[] args)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var appsettings = (args == null || args.Length == 0) ? $"../SBAT.Web/appsettings.json"
                : $"../SBAT.Web/appsettings.{args?[0]}.json";

            IConfigurationRoot configurationRoot =
                new ConfigurationBuilder()
                    .AddJsonFile(Path.GetFullPath(Path.Combine(currentDir, appsettings)))
                    .Build();

            var connectionString = configurationRoot.GetConnectionString("sbatDatabase");
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("EF Core Design Time: sbatDatabase connection string is null");
            }
            var optionsBuilder = new DbContextOptionsBuilder<SBATDbContext>();
            optionsBuilder.UseSqlite(configurationRoot.GetConnectionString("sbatDatabase"));

            return new SBATDbContext(optionsBuilder.Options);
        }
    }
}