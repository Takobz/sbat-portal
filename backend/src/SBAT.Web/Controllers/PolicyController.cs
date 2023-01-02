using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Helpers;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController : Controller
    {
        private readonly IValidationResolver _validationResolver;
        public PolicyController(IValidationResolver validationResolver)
        {
            _validationResolver = validationResolver ?? throw new ArgumentNullException(nameof(validationResolver));
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = RolesConstants.User)]
        public IActionResult CreatePolicyMemeberShip([FromBody] CreatePolicyRequest createPolicy)
        {
            var createPolicyValidator = _validationResolver.GetValidator<CreatePolicyRequest>();
            var validationResult = createPolicyValidator.Validate(createPolicy);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new Response<EmptyResponse> { Errors = validationErrors });
            }

            
            return Ok("Auth'd");
        }

    }
}