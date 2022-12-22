using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SBAT.Core.Entities;

namespace SBAT.Infrastructure.Data
{
    #pragma warning disable CS8618
    public class SBATDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SBATDbContext(DbContextOptions<SBATDbContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Member> Members { get; private set; }
        public DbSet<Plan> Plan { get; private set; }
        public DbSet<Policy> Policies { get; private set; }
    }
}