using Microsoft.AspNetCore.Identity;

namespace SBAT.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstNames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
    }
}