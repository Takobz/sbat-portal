using AutoMapper;
using SBAT.Infrastructure.Data.Repos;
using SBAT.Infrastructure.Identity;
using SBAT.Web.Models.Request;
using SBAT.Web.Services.Common;

namespace SBAT.Web.Services
{
    public class UserService : IUserService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        
        public UserService(ILoginRepository loginRepository, IMapper mapper)
        {
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ServiceResponse<ApplicationUser>> CreateUserAsync(RegisterUserRequest userRequest)
        {
            var username = $"{userRequest.IdentityType}-{userRequest.IdentityNumber}";
            var user = await _loginRepository.GetUserByNameAsync(username);
            if (user is not null)
            {
                var errors = new List<string>() { "User Already Exists" };
                return ServiceResponse<ApplicationUser>.CreateServiceResponse(default, Code.Conflict, errors);
            }

            user = _mapper.Map<ApplicationUser>(userRequest);
            user = await _loginRepository.CreateUserAsync(user, userRequest.ConfirmPassword);
            if (user is null)
            {
                var errors = new List<string>() { "Failed to create user" };
                return ServiceResponse<ApplicationUser>.CreateServiceResponse(default, Code.Conflict, errors);
            }

            if (!(await _loginRepository.AddedUserRoleAsync(user, RolesConstants.User)))
            {
                var errors = new List<string>() { "Failed to add user role" };
                return ServiceResponse<ApplicationUser>.CreateServiceResponse(default, Code.UpdateFail, errors);
            }

            return ServiceResponse<ApplicationUser>.CreateServiceResponse(user, Code.Success, new List<string>());
        }

        public async Task<ServiceResponse<string>> SingInUserAsync(SignInUserRequest signInRequest)
        {
            var userToken = await _loginRepository.SignInUserPasswordAsync(signInRequest.Username, signInRequest.Password);

            return (string.IsNullOrEmpty(userToken)) ? new ServiceResponse<string> { Code = Code.Unauthorized }
                : new ServiceResponse<string> { Response = userToken, Code = Code.Success, Errors = new List<string>()};
        }

        public async Task<ServiceResponse<EmptyServiceResponse>> AssignUserRoleAsync(string username, string role)
        {
            var user = await _loginRepository.GetUserByNameAsync(username);
            if (user is null)
            {
                var errors = new List<string> { $"Username: {username} doesn't exist." };
                return ServiceResponse<EmptyServiceResponse>.CreateServiceResponse(new (), Code.BadRequest, errors);
            }

            if (!await _loginRepository.AddedUserRoleAsync(user, role))
            {
                var errors = new List<string> { $"Failed to add role: {role}." };
                return ServiceResponse<EmptyServiceResponse>.CreateServiceResponse(new (), Code.Conflict, errors);
            }

            return ServiceResponse<EmptyServiceResponse>.CreateServiceResponse(new (), Code.Success, new List<string>());   
        }
    }

    public interface IUserService 
    {
        Task<ServiceResponse<ApplicationUser>> CreateUserAsync(RegisterUserRequest userRequest);
        Task<ServiceResponse<string>> SingInUserAsync(SignInUserRequest signInRequest);
        Task<ServiceResponse<EmptyServiceResponse>> AssignUserRoleAsync(string username, string role);
    }
}