using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        
        public UserService(
            ILoginRepository loginRepository,
            IMapper mapper)
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
                return CreateServiceResponse(default, Code.Conflict, errors);
            }

            user = _mapper.Map<ApplicationUser>(userRequest);
            user = await _loginRepository.CreateUserAsync(user, userRequest.ConfirmPassword);
            if (user is null)
            {
                var errors = new List<string>() { "Failed to create user" };
                return CreateServiceResponse(default, Code.Conflict, errors);
            }

            if (!(await _loginRepository.AddedUserRoleAsync(user, RolesConstants.User)))
            {
                var errors = new List<string>() { "Failed to add user role" };
                return CreateServiceResponse(default, Code.UpdateFail, errors);
            }

            return CreateServiceResponse(user, Code.Success, new List<string>());
        }

        public async Task<ServiceResponse<string>> SingInUserAsync(SignInUserRequest signInRequest)
        {
            var userToken = await _loginRepository.SignInUserPasswordAsync(signInRequest.Username, signInRequest.Password);

            return (string.IsNullOrEmpty(userToken)) ? new ServiceResponse<string> { Code = Code.Unauthorized }
                : new ServiceResponse<string> { Response = userToken, Code = Code.Success, Errors = new List<string>()};
        }

        private ServiceResponse<ApplicationUser> CreateServiceResponse(ApplicationUser? user, Code code, List<string> errors)
        {
            return new ServiceResponse<ApplicationUser>
            {
                Code = code,
                Response = user,
                Errors = errors
            };
        }
    }

    public interface IUserService 
    {
        Task<ServiceResponse<ApplicationUser>> CreateUserAsync(RegisterUserRequest userRequest);
        Task<ServiceResponse<string>> SingInUserAsync(SignInUserRequest signInRequest);
    }
}