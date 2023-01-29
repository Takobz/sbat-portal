using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;
using SBAT.Infrastructure.Interfaces;
using SBAT.Web.Helpers;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class PolicyController : Controller
    {
        private readonly IValidationResolver _validationResolver;
        private readonly IPolicyService _policyService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PolicyController(
            IValidationResolver validationResolver,
            IPolicyService policyService,
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IMapper mapper)
        {
            _validationResolver = validationResolver ?? throw new ArgumentNullException(nameof(validationResolver));
            _policyService = policyService ?? throw new ArgumentNullException(nameof(policyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Authorize(Policy = RolesConstants.User)]
        [Route("create")]
        public async Task<IActionResult> CreatePolicyMemeberShip([FromBody] CreatePolicyRequest createPolicy)
        {
            var createPolicyValidator = _validationResolver.GetValidator<CreatePolicyRequest>();
            var validationResult = createPolicyValidator.Validate(createPolicy);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new Response<EmptyResponse> { Errors = validationErrors });
            }

            if (User.Identity is null || string.IsNullOrEmpty(User?.Identity?.Name))
            {
                var userName = $"{createPolicy.MainMember.IdentityType}-{createPolicy.MainMember.IdentityNumber}";
                return Ok(new Response<EmptyResponse>
                { Errors = new List<string> { $"User with username: {userName} doesn't exist" } });
            }

            var policyNumber = _policyService.GeneratePolicyNumber();
            var policy = _mapper.Map<Policy>(createPolicy);
            policy.CreatePolicyNumber(policyNumber);

            policy.CreateMainMember(User.Identity.Name);
            var user = await _userService.GetUserByUsernameAsync(User.Identity.Name);
            if (user is null)
            {
                return BadRequest(new Response<EmptyResponse>
                { Errors = new List<string> { $"Couldn't get user with username: {User.Identity.Name}" } });
            }
            await _userService.AddedUserRoleAsync(user, RolesConstants.MainMemeber);

            _policyService.CreatePolicy(policy);
            var createdPolicy = _policyService.GetPolicyByPolicyNumber(policyNumber);
            var createdPolicyResponse = _mapper.Map<CreatePolicyResponse>(createdPolicy);

            return Ok(createdPolicyResponse);
        }

        [HttpGet("memberships/{principalMemberUsercode}")]
        [Authorize(Policy = RolesConstants.MainMemeber)]
        public IActionResult GetMemberPolicies(string principalMemberUsercode)
        {
            //TODO make mainmember claim on jwt token creation

            if (string.IsNullOrEmpty(principalMemberUsercode))
            {
                return BadRequest();
            }

            var userPolicies = _policyService.GetUserPolicies(principalMemberUsercode);
            if (userPolicies == null || !userPolicies.Any())
            {
                return NotFound();
            }

            var userPoliciesResponse = userPolicies
                .Select(pol => _mapper.Map<GetPolicyResponse>(pol))
                .ToList();

            return Ok(new Response<ICollection<GetPolicyResponse>> { Data = userPoliciesResponse });
        }

        // [HttpPut]
        // [Authorize(Policy = RolesConstants.MainMemeber)]
        // public IActionResult AddMemeberToPolicy([FromBody] CreateMemberRequest createMember)
        // {

        // }
    }
}