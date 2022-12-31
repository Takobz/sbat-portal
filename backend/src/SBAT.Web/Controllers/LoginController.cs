using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly IValidator<RegisterUserRequest> _registerUserValidator;
        private readonly IMapper _mapper;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            ITokenClaimsService tokenClaimsService,
            IValidator<RegisterUserRequest> registerUserValidator,
            IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenClaimsService = tokenClaimsService ?? throw new ArgumentNullException(nameof(tokenClaimsService));
            _registerUserValidator = registerUserValidator ?? throw new ArgumentNullException(nameof(registerUserValidator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUser)
        {

            var validationResult = _registerUserValidator.Validate(registerUser);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = _mapper.Map<ApplicationUser>(registerUser);
            IdentityResult identityResult = await _userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(err => err.Description).ToList();
                return Ok(new Response<EmptyResponse>
                {
                    Data = new EmptyResponse(),
                    Errors = errors
                });
            }

            var userName = $"{registerUser.IdentityType}-{registerUser.IdentityNumber}";
            user = await _userManager.FindByNameAsync(userName);
            var createdUser = _mapper.Map<UserResponse>(user);

            return Created($"login/{userName}", new Response<UserResponse> { Data = createdUser});
        }

        // I don't like warnings :)
        // [AllowAnonymous]
        // public async Task<IActionResult> Login()
        // {
        //     throw new NotImplementedException();
        // }
    }
}