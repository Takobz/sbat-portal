using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SBAT.Web.Controllers
{
    [Authorize]
    [ApiController]
    public class PolicyController : Controller
    {
        public PolicyController()
        {

        }

        //[HttpPost]

    }
}