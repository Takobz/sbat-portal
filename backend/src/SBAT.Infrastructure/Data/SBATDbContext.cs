using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SBAT.Core.Entities;

namespace SBAT.Infrastructure.Data
{
    #pragma warning disable CS8618
    public class SBATDbContext : IdentityDbContext
    {
        public SBATDbContext(DbContextOptions<SBATDbContext> options) : base(options) {}
        public DbSet<Member> Members { get; private set; }
        public DbSet<Plan> Plan { get; private set; }
        public DbSet<Policy> Policies { get; private set; }
    }
}