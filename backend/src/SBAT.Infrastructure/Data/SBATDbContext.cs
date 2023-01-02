using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole(RolesConstants.User) 
                    {
                        NormalizedName = RolesConstants.User.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new IdentityRole(RolesConstants.MainMemeber)
                    {
                        NormalizedName = RolesConstants.MainMemeber.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });
            base.OnModelCreating(modelBuilder);
        }
    }
}