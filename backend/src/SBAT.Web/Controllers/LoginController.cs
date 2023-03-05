using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Data.Repos;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;
using SBAT.Web.SBATValidation;
using SBAT.Web.Services;
using SBAT.Web.Services.Common;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class LoginController : SBATBaseController<LoginController>
    {
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly IUserService _userService;

        public LoginController(
            ITokenClaimsService tokenClaimsService,
            IUserService userService)
        {
            _tokenClaimsService = tokenClaimsService ?? throw new ArgumentNullException(nameof(tokenClaimsService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [AllowAnonymous]
        [SBATValidation<RegisterUserRequest>]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUser)
        {
            var createUserResponse = await _userService.CreateUserAsync(registerUser);
            if (createUserResponse.Code == Code.Conflict)
            {
                return Conflict(new Response<EmptyResponse>
                {
                    Errors = createUserResponse.Errors
                });
            }

            var createdUser = Mapper.Map<UserResponse>(createUserResponse.Response);
            return Created($"login/{createdUser!.UserName}", new Response<UserResponse> { Data  = createdUser});
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        [SBATValidation<SignInUserRequest>]
        public async Task<IActionResult> SignIn([FromBody] SignInUserRequest signInUser)
        {
            // var signInResult = await _signInManager.PasswordSignInAsync(userName: signInUser.Username, password: signInUser.Password,
            //      isPersistent: false, lockoutOnFailure: false);
            // if(signInResult == null || !signInResult.Succeeded)
            // {
            //     return Unauthorized(new Response<EmptyResponse>
            //     { 
            //         Errors = new List<string> { $"User: {signInUser.Username} is not authorised, please check if password/username is correct" } 
            //     });
            // }

            // var userToken = await _tokenClaimsService.GetTokenAsync(signInUser.Username);
            // var response = new SignInUserResponse 
            // {
            //     Username = signInUser.Username,
            //     JwtToken = userToken
            // };

            // return Ok(new Response<SignInUserResponse>
            // {
            //     Data = response
            // });
            return Ok();
        }

        //Continue Registration - In case some step(s) failed
    }
}