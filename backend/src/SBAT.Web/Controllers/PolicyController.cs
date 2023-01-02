using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Models.Request;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController : Controller
    {
        public PolicyController()
        {

        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = RolesConstants.User)]
        public IActionResult CreatePolicyMemeberShip([FromBody] CreatePolicyRequest createPolicy)
        {
            return Ok("Auth'd");
        }

    }
}