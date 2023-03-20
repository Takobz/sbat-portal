using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
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
    public class PolicyController : SBATBaseController<PolicyController>
    {
        private readonly IPolicyService _policyService;
        private readonly IUserService _userService;

        public PolicyController(
            IPolicyService policyService,
            IUserService userService)
        {
            _policyService = policyService ?? throw new ArgumentNullException(nameof(policyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [Authorize(Policy = RolesConstants.User)]
        [Route("create")]
        [SBATValidation<CreatePolicyRequest>]
        public async Task<IActionResult> CreatePolicyMemeberShip([FromBody] CreatePolicyRequest createPolicy)
        {
            //TODO: Check AssignUserRole response code etc.
            var policy = Mapper.Map<Policy>(createPolicy);
            await _userService.AssignUserRoleAsync(createPolicy.MainMemberUserName, RolesConstants.MainMemeber);

            var policyResponse = _policyService.CreatePolicy(policy);
            if (policyResponse.Code == Code.Conflict)
                return Conflict(new Response<EmptyResponse> { Errors  = policyResponse.Errors });

            if(policyResponse.Response == null)
                return Conflict();

            return Ok(new Response<CreatePolicyResponse> { Data = policyResponse.Response });
        }

        [HttpGet("memberships/{principalMemberUsercode}")]
        [Authorize(Policy = RolesConstants.MainMemeber)]
        public IActionResult GetMemberPolicies(string principalMemberUsercode)
        {
            var userPoliciesResponse = _policyService.GetMemeberPolicies(principalMemberUsercode);
            if (userPoliciesResponse.Response == null || !userPoliciesResponse.Response.Any())
                return NotFound();

            return Ok(new Response<ICollection<GetPolicyResponse>> { Data = userPoliciesResponse.Response });
        }

        // [HttpPut]
        // [Authorize(Policy = RolesConstants.MainMemeber)]
        // public IActionResult AddMemeberToPolicy([FromBody] CreateMemberRequest createMember)
        // {

        // }
    }
}