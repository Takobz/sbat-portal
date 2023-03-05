using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;
using SBAT.Web.SBATValidation;
using SBAT.Web.Services;

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
            // var policy = Mapper.Map<Policy>(createPolicy);
            // var user = await _userService.GetUserByUsernameAsync(createPolicy.MainMemberUserName);
            // if (user is null)
            // {
            //     return BadRequest(new Response<EmptyResponse>
            //     { Errors = new List<string> { $"Couldn't get user with username: {createPolicy.MainMemberUserName}" } });
            // }
            // await _userService.AddedUserRoleAsync(user, RolesConstants.MainMemeber);

            // var createdPolicy = _policyService.CreatePolicy(policy);
            // var createdPolicyResponse = Mapper.Map<CreatePolicyResponse>(createdPolicy);
            // return Ok(new Response<CreatePolicyResponse> { Data = createdPolicyResponse});

            return Ok();
        }

        [HttpGet("memberships/{principalMemberUsercode}")]
        [Authorize(Policy = RolesConstants.MainMemeber)]
        public IActionResult GetMemberPolicies(string principalMemberUsercode)
        {
            // if (string.IsNullOrEmpty(principalMemberUsercode))
            // {
            //     return BadRequest();
            // }

            // //include memebers on call!
            // var userPolicies = _policyService.GetUserPolicies(principalMemberUsercode);
            // if (userPolicies == null || !userPolicies.Any())
            // {
            //     return NotFound();
            // }

            // var userPoliciesResponse = userPolicies
            //     .Select(pol => Mapper.Map<GetPolicyResponse>(pol))
            //     .ToList();

            // return Ok(new Response<ICollection<GetPolicyResponse>> { Data = userPoliciesResponse });
            return Ok();
        }

        // [HttpPut]
        // [Authorize(Policy = RolesConstants.MainMemeber)]
        // public IActionResult AddMemeberToPolicy([FromBody] CreateMemberRequest createMember)
        // {

        // }
    }
}