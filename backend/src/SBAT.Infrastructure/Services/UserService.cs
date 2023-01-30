using Microsoft.AspNetCore.Identity;
using SBAT.Infrastructure.Identity;
using SBAT.Infrastructure.Interfaces;

namespace SBAT.Infrastructure.Services 
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task AddedUserRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<ApplicationUser?> GetUserByUsernameAsync(string userIdentity)
        {
            return await _userManager.FindByNameAsync(userIdentity);
        }
    }
}