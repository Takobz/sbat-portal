using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;

namespace SBAT.Infrastructure.Services
{
    public class TokenClaimsService : ITokenClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public TokenClaimsService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        }
        
        public async Task<string> GetTokenAsync(string userName)
        {
            var securityKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenExpirationTimeInMinutes = 30;

            var user = await _userManager.FindByNameAsync(userName);
            if(user == null) return string.Empty;
            var claims = await GenerateUserClaims(user);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenExpirationTimeInMinutes),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task< List<Claim>> GenerateUserClaims(ApplicationUser user)
        {
            if(string.IsNullOrEmpty(user.UserName))
            {
                return new List<Claim>();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}