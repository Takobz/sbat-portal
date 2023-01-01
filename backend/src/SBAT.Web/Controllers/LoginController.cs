using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Helpers;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly IValidationResolver _validatorResolver;
        private readonly IMapper _mapper;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenClaimsService tokenClaimsService,
            IValidationResolver validatorResolver,
            IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenClaimsService = tokenClaimsService ?? throw new ArgumentNullException(nameof(tokenClaimsService));
            _validatorResolver = validatorResolver ?? throw new ArgumentNullException(nameof(validatorResolver));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //TODO: Expect Hashed password from client.
        //TODO: Move this logic into a seperate service i.e) ILoginService.cs
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUser)
        {
            var registerUserValidator = _validatorResolver.GetValidator<RegisterUserRequest>();
            var validationErrors = registerUserValidator.ValidateModelAndGetErrorMessages(registerUser);
            if (validationErrors != null && validationErrors.Any())
            {
                return BadRequest(new Response<EmptyResponse> { Errors = validationErrors });
            }

            var userName = $"{registerUser.IdentityType}-{registerUser.IdentityNumber}";
            var user = await _userManager.FindByNameAsync(userName);
            if (user is not null)
            {
                return Conflict(new Response<EmptyResponse>
                {
                    Errors = new List<string> 
                    { 
                        $"User with Identity: {registerUser.IdentityNumber} and Identity Type: {registerUser.IdentityType} exits"
                    }
                });
            }

            user = _mapper.Map<ApplicationUser>(registerUser);
            IdentityResult identityResult = await _userManager.CreateAsync(user, registerUser.ConfirmPassword);
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(err => err.Description).ToList();
                return Ok(new Response<EmptyResponse>
                {
                    Data = new EmptyResponse(),
                    Errors = errors
                });
            }

            user = await _userManager.FindByNameAsync(userName);
            var createdUser = _mapper.Map<UserResponse>(user);

            return Created($"login/{userName}", new Response<UserResponse> { Data = createdUser});
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserRequest signInUser)
        {
            var signInValidaor = _validatorResolver.GetValidator<SignInUserRequest>();
            var validationErrors = signInValidaor.ValidateModelAndGetErrorMessages(signInUser);
            if(validationErrors != null && validationErrors.Any())
            {
                return BadRequest(new Response<EmptyResponse>{ Errors = validationErrors });
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userName: signInUser.Username, password: signInUser.Password,
                 isPersistent: false, lockoutOnFailure: false);
            if(signInResult == null || !signInResult.Succeeded)
            {
                return Unauthorized(new Response<EmptyResponse>
                { 
                    Errors = new List<string> { $"User: {signInUser.Username} is not authorised, please check if password/username is correct" } 
                });
            }

            var userToken = await _tokenClaimsService.GetTokenAsync(signInUser.Username);
            var response = new SignInUserResponse 
            {
                Username = signInUser.Username,
                JwtToken = userToken
            };

            return Ok(new Response<SignInUserResponse>
            {
                Data = response
            });
        }
    }
}