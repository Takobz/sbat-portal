using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<UserRequest> _userRequestValidator;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IValidator<UserRequest> userRequestValidator)
        {
            _userService = userService;
            _mapper = mapper;
            _userRequestValidator = userRequestValidator;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                return NotFound();
            }
            
            var userDto = _mapper.Map<UserResponse>(user);
            return Ok(userDto);
        }

        //TODO: Add ExceptionHandler extension!
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequest userRequest)
        {
            var validationResult = _userRequestValidator.Validate(userRequest);
            if(!validationResult.IsValid)
            {
                return BadRequest($"{validationResult.Errors}");
            }
            
            var user = _mapper.Map<User>(userRequest);
            _userService.AddUser(user);

            user = _userService.GetUser(x => x.IdentityNumber == user.IdentityNumber);
            var createdUser = _mapper.Map<UserResponse>(user);
            return Created($"/{user?.Id}", createdUser);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateUserFirstNames(int id, [FromBody] UpdateFirstNamesRequest updateFirstNamesRequest)
        {
            var user = _userService.GetUserById(id);
            //It's Xmas!!

            return NoContent();
        }
    }
}