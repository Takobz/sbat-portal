using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;

namespace SBAT.Infrastructure.Data.Repos
{
    public class LoginRepository : ILoginRepository 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenService;
        private readonly ILogger<LoginRepository> _logger;

        public LoginRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenClaimsService tokenService,
            ILogger<LoginRepository> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApplicationUser?> GetUserByNameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser?> CreateUserAsync(ApplicationUser user, string password)
        {
            if(string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(password))
                return null;

            var identityResult = await _userManager.CreateAsync(user, password);
            if (!identityResult.Succeeded)
            {
                _logger.LogError("User creation failed with Errors: {0}", identityResult.Errors);
                return null;
            }
                

            return await _userManager.FindByNameAsync(user.UserName);
        }

        public async Task<bool> AddedUserRoleAsync(ApplicationUser user, string role)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(role))
            {
                _logger.LogError("Username or Role to be added is empty.");
                return false;
            }  

            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                _logger.LogError($"Failed to set role: {role} with errors: {roleResult.Errors}");
                return false;
            }

            return true;
        }

        public async Task<string> SignInUserPasswordAsync(string username, string password)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(username, password,
             isPersistent: false, lockoutOnFailure: false);

            if (signInResult is null || !signInResult.Succeeded)
            {
                _logger.LogWarning($"User: {username} failed to sign in");
                return string.Empty;
            } 

            _logger.LogInformation($"{username} signed in successfully returning token");
            return await _tokenService.GetTokenAsync(username) ?? string.Empty;
        }
    }

    public interface ILoginRepository 
    {
        Task<ApplicationUser?> GetUserByNameAsync(string username);
        Task<ApplicationUser?> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> AddedUserRoleAsync(ApplicationUser user, string role);
        Task<string> SignInUserPasswordAsync(string username, string password);
    }
}