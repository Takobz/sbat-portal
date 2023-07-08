using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Web.Models.Common;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;
using SBAT.Web.SBATValidation;
using SBAT.Web.Services;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api")]
    public class LoginController : SBATBaseController<LoginController>
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [AllowAnonymous]
        [SBATValidation<RegisterUserRequest>]
        [Route("login/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUser)
        {
            var createUserResponse = await _userService.CreateUserAsync(registerUser);
            if (createUserResponse.Code == ResponseCode.Conflict)
            {
                var response = Response<EmptyResponse>.CreateResponse(new EmptyResponse(), createUserResponse.Errors, ResponseCode.Conflict);
                return Conflict(response);
            }

            var createdUser = Mapper.Map<UserResponse>(createUserResponse.Response);
            return Created($"login/{createdUser!.UserName}", Response<UserResponse>.CreateResponse(createdUser, new List<string>(), ResponseCode.Success));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login/sign-in")]
        [SBATValidation<SignInUserRequest>]
        public async Task<IActionResult> SignIn([FromBody] SignInUserRequest signInUser)
        {
            var tokenResponse = await _userService.SingInUserAsync(signInUser);

            if (string.IsNullOrEmpty(tokenResponse.Response))
                return Unauthorized();

            var responsedata = new SignInUserResponse { Username = signInUser.Username, JwtToken = tokenResponse.Response };
            return Ok(Response<SignInUserResponse>.CreateResponse(responsedata, new List<string>(), ResponseCode.Success));
        }

        //TODO: Continue Registration - In case some step(s) failed
    }
}