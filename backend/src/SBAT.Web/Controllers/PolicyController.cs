using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
using SBAT.Infrastructure.Identity;
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
        [Route("policy/create")]
        [SBATValidation<CreatePolicyRequest>]
        public async Task<IActionResult> CreatePolicyMemeberShip([FromBody] CreatePolicyRequest createPolicy)
        {
            //TODO: Check AssignUserRole response code etc.
            var policy = Mapper.Map<Policy>(createPolicy);
            await _userService.AssignUserRoleAsync(createPolicy.MainMemberUserName, RolesConstants.MainMemeber);

            var policyResponse = _policyService.CreatePolicy(policy);
            if (policyResponse.Code == ResponseCode.Conflict)
                return Conflict(new Response<EmptyResponse> { Errors  = policyResponse.Errors });

            if(policyResponse.Response == null)
                return Conflict();

            return Ok(new Response<CreatePolicyResponse> { Data = policyResponse.Response });
        }

        [HttpGet("policy/{policyNumber}")]
        [Authorize(Policy = RolesConstants.MainMemeber)]
        public IActionResult AddMemeberToPolicy(string policyNumber)
        {
            //TODO: Make sure they are main member for this policy
            var responsePolicy = _policyService.GetPolicy(policyNumber.ToUpper());
            if (responsePolicy.Code == ResponseCode.NotFound || responsePolicy.Response == default)
                return NotFound(new Response<EmptyResponse> { Errors = responsePolicy.Errors });

            return Ok(new Response<GetPolicyResponse> { Data = responsePolicy.Response } );
        }

        [HttpGet("policy/all/{principalMemberUsercode}")]
        [Authorize(Policy = RolesConstants.MainMemeber)]
        public IActionResult GetMemberPolicies(string principalMemberUsercode)
        {
            var userPoliciesResponse = _policyService.GetMemeberPolicies(principalMemberUsercode);
            if (userPoliciesResponse.Response == null || !userPoliciesResponse.Response.Any())
                return NotFound(new Response<EmptyResponse> { Errors = userPoliciesResponse.Errors });

            return Ok(new Response<ICollection<GetPolicyResponse>> { Data = userPoliciesResponse.Response });
        }

        [HttpPut("policy/{policyNumber}")]
        [Authorize(Policy = RolesConstants.MainMemeber)]
        [SBATValidation<CreateMemberRequest>]
        public IActionResult AddMemeberToPolicy(string policyNumber, [FromBody] CreateMemberRequest createMember)
        {
            var responsePolicy = _policyService.AddMemeberToPolicy(policyNumber.ToUpper(), createMember);
            if (responsePolicy.Code == ResponseCode.NotFound || responsePolicy.Response == null)
                return NotFound(new Response<EmptyResponse> { Errors = responsePolicy.Errors });
            
            return Ok(new Response<GetPolicyResponse> { Data = responsePolicy.Response });
        }
    }
}