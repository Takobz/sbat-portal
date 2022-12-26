using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SBAT.Core.Entities;
using SBAT.Core.Interfaces;
using SBAT.Web.Models.Request;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Controllers
{
    [ApiController]
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
                return NotFound($"User with id:{id} doesn't exist");
            }
            
            var userDto = _mapper.Map<UserResponse>(user);
            return Ok(userDto);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            var usersDto = users.Select(u => _mapper.Map<UserResponse>(u));
            return Ok(usersDto);
        }

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
            if (user == null)
            {
                return NotFound($"User with id:{id} doesn't exist");
            }

            user!.ChangeFirstNames(updateFirstNamesRequest.FirstNames);
            _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                return NotFound($"User with id:{id} doesn't exist");
            }
            _userService.DeleteUser(user);

            return NoContent();
        }
    }
}