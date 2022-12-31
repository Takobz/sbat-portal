using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SBAT.Core.Entities;
using SBAT.Infrastructure.Identity;

namespace SBAT.Infrastructure.Data
{
    #pragma warning disable CS8618
    public class SBATDbContext : IdentityDbContext
    {
        public SBATDbContext(DbContextOptions<SBATDbContext> options) : base(options) {}
        public DbSet<ApplicationUser> ApplicationUsers { get; private set; }
        public DbSet<Member> Members { get; private set; }
        public DbSet<Plan> Plan { get; private set; }
        public DbSet<Policy> Policies { get; private set; }
    }
}