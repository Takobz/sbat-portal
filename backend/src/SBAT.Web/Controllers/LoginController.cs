using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Interfaces;
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
            var tokenResponse = await _userService.SingInUserAsync(signInUser);

            if (string.IsNullOrEmpty(tokenResponse.Response))
                return Unauthorized();

            return Ok(new Response<SignInUserResponse>
            {
                Data = new SignInUserResponse { Username = signInUser.Username, JwtToken = tokenResponse.Response }
            });
        }

        //TODO: Continue Registration - In case some step(s) failed
    }
}