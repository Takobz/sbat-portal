using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SBAT.Web.Models.Request;

namespace SBAT.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IValidator<UserRequest> _userRequestValidator;

        public UsersController(
            IMapper mapper,
            IValidator<UserRequest> userRequestValidator)
        {
            _mapper = mapper;
            _userRequestValidator = userRequestValidator;
        }
        
        
    }
}