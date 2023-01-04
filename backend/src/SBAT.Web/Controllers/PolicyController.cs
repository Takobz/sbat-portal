using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PolicyController(
            IValidationResolver validationResolver,
            IPolicyService policyService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _validationResolver = validationResolver ?? throw new ArgumentNullException(nameof(validationResolver));
            _policyService = policyService ?? throw new ArgumentNullException(nameof(policyService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Authorize(Policy = RolesConstants.User)]
        [Route("create")]
        public IActionResult CreatePolicyMemeberShip([FromBody] CreatePolicyRequest createPolicy)
        {
            var createPolicyValidator = _validationResolver.GetValidator<CreatePolicyRequest>();
            var validationResult = createPolicyValidator.Validate(createPolicy);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new Response<EmptyResponse> { Errors = validationErrors });
            }

            var policyNumber = _policyService.GeneratePolicyNumber();
            var policy = _mapper.Map<Policy>(createPolicy);
            policy.CreatePolicyNumber(policyNumber);
            if (User.Identity is null || string.IsNullOrEmpty(User?.Identity?.Name))
            {
                var userName = $"{createPolicy.MainMember.IdentityType}-{createPolicy.MainMember.IdentityNumber}";
                var user = _userManager.FindByNameAsync(userName);
                if (user is null)
                {
                    return Ok(new Response<EmptyResponse> { Errors = new List<string> { $"User with username: {userName} doesn't exist" } });
                }
                policy.CreateMainMember(userName);
            }
            else
            {
                policy.CreateMainMember(User.Identity.Name);
            }

            _policyService.CreatePolicy(policy);
            var createdPolicy = _policyService.GetPolicyByPolicyNumber(policyNumber);
            var createdPolicyResponse = _mapper.Map<CreatePolicyResponse>(createdPolicy);
            return Ok(createdPolicyResponse);
        }
    }
}