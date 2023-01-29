using SBAT.Infrastructure.Identity;

namespace SBAT.Infrastructure.Interfaces
{
    public interface IUserService 
    {
        Task AddedUserRoleAsync(ApplicationUser user, string role);
        Task<ApplicationUser> GetUserByUsernameAsync(string userIdentity);
    }
}